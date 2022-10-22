DO
$do$
BEGIN
   IF not exists (select 1 from genre as g where g.description = 'Menina') THEN
      INSERT INTO public.genre(id, description)	VALUES (1, 'Menina');
   END IF;
   
   IF not exists (select 1 from genre as g where g.description = 'Menino') THEN
      INSERT INTO public.genre(id, description)	VALUES (2, 'Menino');
   END IF;
END
$do$
