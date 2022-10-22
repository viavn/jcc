DO
$do$
BEGIN
   IF not exists (select 1 from legal_person_type as g where g.description = 'Mãe') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (1, 'Mãe');
   END IF;
   
   IF not exists (select 1 from legal_person_type as g where g.description = 'Pai') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (2, 'Pai');
   END IF;
   
   IF not exists (select 1 from legal_person_type as g where g.description = 'Tia/Tio') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (3, 'Tia/Tio');
   END IF;

   IF not exists (select 1 from legal_person_type as g where g.description = 'Irmã/Irmão') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (4, 'Irmã/Irmão');
   END IF;

   IF not exists (select 1 from legal_person_type as g where g.description = 'Avó/Avô') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (5, 'Avó/Avô');
   END IF;

   IF not exists (select 1 from legal_person_type as g where g.description = 'Madrasta/Padastro') THEN
      INSERT INTO public.legal_person_type(id, description) VALUES (6, 'Madrasta/Padastro');
   END IF;
END
$do$
