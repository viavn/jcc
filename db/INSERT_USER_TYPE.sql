DO
$do$
BEGIN
   IF not exists (select 1 from user_type as g where g.description = 'Administrador') THEN
      INSERT INTO public.user_type(id, description) VALUES (1, 'Administrador');
   END IF;
   
   IF not exists (select 1 from user_type as g where g.description = 'Padrão') THEN
      INSERT INTO public.user_type(id, description) VALUES (2, 'Padrão');
   END IF;
END
$do$
