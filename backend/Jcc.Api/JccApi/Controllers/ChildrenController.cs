using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities.Dtos;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChildrenController : JccBaseController
    {
        private readonly ILogger<ChildrenController> _logger;
        private readonly ICreateChildUseCaseAsync _createChildUseCaseAsync;
        private readonly IUpdateChildUseCaseAsync _updateChildUseCaseAsync;
        private readonly IDeleteChildUseCaseAsync _deleteChildUseCaseAsync;
        private readonly ICreateGiftUseCaseAsync _createGiftUseCaseAsync;
        private readonly IUpdateGiftUseCaseAsync _updateGiftUseCaseAsync;
        private readonly IDeleteGiftUseCaseAsync _deleteGiftUseCaseAsync;
        private readonly IChildRepository _childRepository;

        public ChildrenController(
            ILogger<ChildrenController> logger,
            ICreateChildUseCaseAsync createChildUseCaseAsync,
            IUpdateChildUseCaseAsync updateChildUseCaseAsync,
            IDeleteChildUseCaseAsync deleteChildUseCaseAsync,
            ICreateGiftUseCaseAsync createGiftUseCaseAsync,
            IUpdateGiftUseCaseAsync updateGiftUseCaseAsync,
            IDeleteGiftUseCaseAsync deleteGiftUseCaseAsync,
            IChildRepository childRepository)
        {
            _logger = logger;
            _createChildUseCaseAsync = createChildUseCaseAsync;
            _updateChildUseCaseAsync = updateChildUseCaseAsync;
            _deleteChildUseCaseAsync = deleteChildUseCaseAsync;
            _createGiftUseCaseAsync = createGiftUseCaseAsync;
            _updateGiftUseCaseAsync = updateGiftUseCaseAsync;
            _deleteGiftUseCaseAsync = deleteGiftUseCaseAsync;
            _childRepository = childRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<ChildGiftDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var children = await _childRepository.GetAllWithDeliveredInformation();
            return Ok(new ApiResult<IEnumerable<ChildGiftDto>>(children));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResult<GetChildrenByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChild(Guid id)
        {
            var child = await _childRepository.GetById(id);
            if (child is null)
            {
                return NotFound();
            }

            return Ok(new ApiResult<GetChildrenByIdResponse>(child));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateChild([FromBody] CreateChildRequest request)
        {
            var id = await _createChildUseCaseAsync.Execute(request);

            return CreatedAtAction(
                nameof(GetChild), new { id },
                new { request.Age, request.ClotheSize, request.FamilyId, request.Genre, request.Name, request.ShoeSize, Id = id }
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateChild([FromRoute] Guid id, [FromBody] UpdateChildRequest request)
        {
            try
            {
                request.Id = id;
                await _updateChildUseCaseAsync.Execute(request);
                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteChild([FromRoute] Guid id)
        {
            try
            {
                await _deleteChildUseCaseAsync.Execute(new DeleteChildRequest(id));
                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/gifts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddGift([FromRoute] Guid id, [FromBody] CreateGiftRequest request)
        {
            try
            {
                request.ChildId = id;
                var godParentId = await _createGiftUseCaseAsync.Execute(request);

                return CreatedAtAction(
                    nameof(GetChild), new { id = request.ChildId },
                    new { request.ChildId, GodParentId = godParentId, request.GodParent, request.Type, request.UserId }
                );
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpPatch("{id}/gifts/{giftTypetId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeliverGift([FromRoute] Guid id, [FromRoute] int giftTypetId, [FromBody] UpdateGiftRequest request)
        {
            try
            {
                request.ChildId = id;
                request.GiftType = (Enums.GiftType)giftTypetId;
                await _updateGiftUseCaseAsync.Execute(request);

                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpDelete("{id}/god-parents/{godParentId}/gifts/{giftTypetId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGift([FromRoute] Guid id, [FromRoute] int giftTypetId, [FromRoute] Guid godParentId)
        {
            try
            {
                var request = new UpdateGiftRequest
                {
                    ChildId = id,
                    GiftType = (Enums.GiftType)giftTypetId,
                    GodParentId = godParentId
                };
                await _deleteGiftUseCaseAsync.Execute(request);

                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> GetChildrenReport()
        {
            var columns = new string[] { "Família", "Criança", "Idade", "Tam. Roupa", "Tam. Calçado", "Qtd. Presentes Entregues", "Qtd. Presentes Faltantes" };
            var children = await _childRepository.GetAllWithDeliveredInformation();

            using (var stream = new MemoryStream())
            {
                using (var workbook = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                {
                    List<OpenXmlAttribute> attributeList;
                    OpenXmlWriter writer;

                    workbook.AddWorkbookPart();
                    WorksheetPart workSheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();

                    writer = OpenXmlWriter.Create(workSheetPart);
                    writer.WriteStartElement(new Worksheet());
                    writer.WriteStartElement(new SheetData());

                    attributeList = new List<OpenXmlAttribute>();
                    // this is the row index
                    int rowIndex = 1;
                    attributeList.Add(new OpenXmlAttribute("r", null, rowIndex.ToString()));

                    writer.WriteStartElement(new Row(), attributeList);

                    for (int j = 0; j < columns.Length; ++j)
                    {
                        attributeList = new List<OpenXmlAttribute>();
                        // this is the data type ("t"), with CellValues.String ("str")
                        attributeList.Add(new OpenXmlAttribute("t", null, "str"));

                        writer.WriteStartElement(new Cell(), attributeList);
                        writer.WriteElement(new CellValue(columns[j]));

                        // this is for Cell
                        writer.WriteEndElement();
                    }

                    // this is for Row
                    writer.WriteEndElement();

                    // VALUES
                    foreach (var child in children)
                    {
                        rowIndex++;
                        attributeList = new List<OpenXmlAttribute>();
                        // this is the row index
                        attributeList.Add(new OpenXmlAttribute("r", null, rowIndex.ToString()));

                        writer.WriteStartElement(new Row(), attributeList);

                        // ### 1a célula
                        attributeList = CreateCell(writer, child.FamilyCode);
                        writer.WriteEndElement();
                        // ### FIM 1a célula

                        // ### 2a célula
                        attributeList = CreateCell(writer, child.Name);
                        writer.WriteEndElement();
                        // ### FIM 2a célula

                        // ### 3a célula
                        attributeList = CreateCell(writer, child.Age);
                        writer.WriteEndElement();
                        // ### FIM 3a célula

                        // ### 4a célula
                        attributeList = CreateCell(writer, child.ClotheSize);
                        writer.WriteEndElement();
                        // ### FIM 4a célula

                        // ### 5a célula
                        attributeList = CreateCell(writer, child.ShoeSize);
                        writer.WriteEndElement();
                        // ### FIM 5a célula

                        // ### 6a célula
                        attributeList = CreateCell(writer, child.DeliveredGifts.ToString());
                        writer.WriteEndElement();
                        // ### FIM 6a célula

                        // ### 7a célula
                        attributeList = CreateCell(writer, child.RemainingGifts.ToString());
                        writer.WriteEndElement();
                        // ### FIM 7a célula

                        // this is for Row
                        writer.WriteEndElement();
                    }

                    // this is for SheetData
                    writer.WriteEndElement();
                    // this is for Worksheet
                    writer.WriteEndElement();
                    writer.Close();

                    writer = OpenXmlWriter.Create(workbook.WorkbookPart);
                    writer.WriteStartElement(new Workbook());
                    writer.WriteStartElement(new Sheets());

                    writer.WriteElement(new Sheet()
                    {
                        Name = "Sheet1",
                        SheetId = 1,
                        Id = workbook.WorkbookPart.GetIdOfPart(workSheetPart)
                    });

                    writer.WriteEndElement(); // Write end for WorkSheet Element
                    writer.WriteEndElement(); // Write end for WorkBook Element
                    writer.Close();

                    workbook.Close();

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "criancas.xlsx");
                }
            }

            static List<OpenXmlAttribute> CreateCell(OpenXmlWriter writer, string cellValue)
            {
                List<OpenXmlAttribute> attributeList = new List<OpenXmlAttribute>();
                attributeList.Add(new OpenXmlAttribute("t", null, "str"));

                writer.WriteStartElement(new Cell(), attributeList);
                writer.WriteElement(new CellValue(cellValue));
                return attributeList;
            }
        }

        //     [HttpPost("batch")]
        //     public async Task<IActionResult> AddChild([FromBody] List<ChildModel> requests)
        //     {
        //         foreach (var request in requests)
        //         {
        //             var child = new Child_Old(request.Name, request.Age, request.ClothesSize, request.ShoeSize, request.LegalResponsible,
        //             request.FamilyAcronym, request.FamilyPhone, request.FamilyAddress);

        //             await _childRepository.Create(child);
        //         }

        //         return CreatedAtAction(nameof(GetChildren), null);
        //     }
    }
}
