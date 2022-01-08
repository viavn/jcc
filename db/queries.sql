select *
 from god_parents   a
      join children b on a."ChildId" = b.id
where a."ChildId" = 'a5896895-3f9d-4138-8ccc-5c5cc53412df'
group by b.id
	  
select count(*) from children
 
SELECT *
FROM god_parents        t
     right join children b on t."ChildId" = b.id
JOIN LATERAL(VALUES('is_clothes_selected',t.is_clothes_selected),('is_shoes_selected',t.is_shoes_selected),('is_gift_selected',t.is_gift_selected)) s(col_name, col_value) ON TRUE
where t."ChildId" is null
group by t."ChildId"

