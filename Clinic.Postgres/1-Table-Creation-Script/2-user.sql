CREATE TABLE public.userbase
(
    id bigserial NOT NULL,
    subject_id bigserial NOT NULL,
    username character(64) NOT NULL,
    password character(1024) NOT NULL,
    provider_name character(64),
    provider_subject_id character(64),
    is_active boolean NOT NULL,
    PRIMARY KEY (id),
    UNIQUE (subject_id),
    UNIQUE (username)
)
WITH (
    OIDS = FALSE
);

ALTER TABLE public.userbase
    OWNER to postgres;