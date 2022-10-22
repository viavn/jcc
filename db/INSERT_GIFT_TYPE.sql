DO
$do$
BEGIN
   IF not exists (select 1 from gift_type as g where g.description = 'Calçado') THEN
      INSERT INTO public.gift_type(id, description) VALUES (1, 'Calçado');
   END IF;
   
   IF not exists (select 1 from gift_type as g where g.description = 'Roupa') THEN
      INSERT INTO public.gift_type(id, description) VALUES (2, 'Roupa');
   END IF;
   
   IF not exists (select 1 from gift_type as g where g.description = 'Presente') THEN
      INSERT INTO public.gift_type(id, description) VALUES (3, 'Presente');
   END IF;
END
$do$
