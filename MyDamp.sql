PGDMP      -         
        }         	   BelazTest    17.2    17.2 %    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    20126 	   BelazTest    DATABASE        CREATE DATABASE "BelazTest" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "BelazTest";
                     postgres    false            �            1259    20171    refreshtokens    TABLE     �   CREATE TABLE public.refreshtokens (
    id integer NOT NULL,
    user_id integer,
    token character varying NOT NULL,
    expires timestamp without time zone
);
 !   DROP TABLE public.refreshtokens;
       public         heap r       postgres    false            �            1259    20170    refreshtokens_id_seq    SEQUENCE     �   CREATE SEQUENCE public.refreshtokens_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.refreshtokens_id_seq;
       public               postgres    false    224            �           0    0    refreshtokens_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.refreshtokens_id_seq OWNED BY public.refreshtokens.id;
          public               postgres    false    223            �            1259    20139    roles    TABLE     a   CREATE TABLE public.roles (
    id integer NOT NULL,
    name character varying(256) NOT NULL
);
    DROP TABLE public.roles;
       public         heap r       postgres    false            �            1259    20138    roles_id_seq    SEQUENCE     �   CREATE SEQUENCE public.roles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.roles_id_seq;
       public               postgres    false    220            �           0    0    roles_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.roles_id_seq OWNED BY public.roles.id;
          public               postgres    false    219            �            1259    20147 
   user_roles    TABLE     x   CREATE TABLE public.user_roles (
    role_id integer NOT NULL,
    user_id integer NOT NULL,
    id integer NOT NULL
);
    DROP TABLE public.user_roles;
       public         heap r       postgres    false            �            1259    20162    user_role_id_seq    SEQUENCE     �   CREATE SEQUENCE public.user_role_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.user_role_id_seq;
       public               postgres    false    221            �           0    0    user_role_id_seq    SEQUENCE OWNED BY     F   ALTER SEQUENCE public.user_role_id_seq OWNED BY public.user_roles.id;
          public               postgres    false    222            �            1259    20128    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(256) NOT NULL,
    passwordhash character varying(256),
    department character varying(256)
);
    DROP TABLE public.users;
       public         heap r       postgres    false            �            1259    20127    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public               postgres    false    218            �           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public               postgres    false    217            3           2604    20174    refreshtokens id    DEFAULT     t   ALTER TABLE ONLY public.refreshtokens ALTER COLUMN id SET DEFAULT nextval('public.refreshtokens_id_seq'::regclass);
 ?   ALTER TABLE public.refreshtokens ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    223    224    224            1           2604    20142    roles id    DEFAULT     d   ALTER TABLE ONLY public.roles ALTER COLUMN id SET DEFAULT nextval('public.roles_id_seq'::regclass);
 7   ALTER TABLE public.roles ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    220    219    220            2           2604    20163    user_roles id    DEFAULT     m   ALTER TABLE ONLY public.user_roles ALTER COLUMN id SET DEFAULT nextval('public.user_role_id_seq'::regclass);
 <   ALTER TABLE public.user_roles ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    222    221            0           2604    20131    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    217    218    218            �          0    20171    refreshtokens 
   TABLE DATA           D   COPY public.refreshtokens (id, user_id, token, expires) FROM stdin;
    public               postgres    false    224   J(       �          0    20139    roles 
   TABLE DATA           )   COPY public.roles (id, name) FROM stdin;
    public               postgres    false    220   �(       �          0    20147 
   user_roles 
   TABLE DATA           :   COPY public.user_roles (role_id, user_id, id) FROM stdin;
    public               postgres    false    221   �(       �          0    20128    users 
   TABLE DATA           G   COPY public.users (id, username, passwordhash, department) FROM stdin;
    public               postgres    false    218   �(       �           0    0    refreshtokens_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.refreshtokens_id_seq', 1, true);
          public               postgres    false    223            �           0    0    roles_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.roles_id_seq', 1, true);
          public               postgres    false    219            �           0    0    user_role_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.user_role_id_seq', 1, true);
          public               postgres    false    222            �           0    0    users_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.users_id_seq', 1, true);
          public               postgres    false    217            ?           2606    20178     refreshtokens refreshtokens_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.refreshtokens
    ADD CONSTRAINT refreshtokens_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.refreshtokens DROP CONSTRAINT refreshtokens_pkey;
       public                 postgres    false    224            9           2606    20144    roles roles_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.roles DROP CONSTRAINT roles_pkey;
       public                 postgres    false    220            ;           2606    20146    roles uq_name 
   CONSTRAINT     H   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT uq_name UNIQUE (name);
 7   ALTER TABLE ONLY public.roles DROP CONSTRAINT uq_name;
       public                 postgres    false    220            5           2606    20137    users uq_username 
   CONSTRAINT     P   ALTER TABLE ONLY public.users
    ADD CONSTRAINT uq_username UNIQUE (username);
 ;   ALTER TABLE ONLY public.users DROP CONSTRAINT uq_username;
       public                 postgres    false    218            =           2606    20169    user_roles user_role_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_role_pkey PRIMARY KEY (id);
 C   ALTER TABLE ONLY public.user_roles DROP CONSTRAINT user_role_pkey;
       public                 postgres    false    221            7           2606    20135    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public                 postgres    false    218            B           2606    20179 (   refreshtokens refreshtokens_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.refreshtokens
    ADD CONSTRAINT refreshtokens_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);
 R   ALTER TABLE ONLY public.refreshtokens DROP CONSTRAINT refreshtokens_user_id_fkey;
       public               postgres    false    218    224    4663            @           2606    20152 !   user_roles user_role_role_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_role_role_id_fkey FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 K   ALTER TABLE ONLY public.user_roles DROP CONSTRAINT user_role_role_id_fkey;
       public               postgres    false    221    220    4665            A           2606    20157 !   user_roles user_role_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_role_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;
 K   ALTER TABLE ONLY public.user_roles DROP CONSTRAINT user_role_user_id_fkey;
       public               postgres    false    4663    221    218            �   Y   x�3�4�N)O�r��L1��7
�r6��sw*(��2�1�I*��v��w��4202�50�52P00�21�22ճ43124����� �t�      �      x�3�L,������� ��      �      x�3�4�4����� �X      �   I   x�3�tN�K��H55�2p13-N.v�0���u�,t�s��r����O.N�,v)
wv�弰��b���� �l     