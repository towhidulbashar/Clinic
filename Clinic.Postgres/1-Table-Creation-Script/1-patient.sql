CREATE TABLE public.patient
(
    address character varying(10240) COLLATE pg_catalog."default",
    age smallint,
    date_of_birth date,
    gender character varying(56) COLLATE pg_catalog."default",
    id bigint NOT NULL DEFAULT nextval('patient_id_seq'::regclass),
    mobile_number character varying(16) COLLATE pg_catalog."default",
    name character varying(1024) COLLATE pg_catalog."default",
    occupation character varying(1024) COLLATE pg_catalog."default",
    blood_group character varying(8) COLLATE pg_catalog."default",
    CONSTRAINT patient_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.patient
    OWNER to postgres;