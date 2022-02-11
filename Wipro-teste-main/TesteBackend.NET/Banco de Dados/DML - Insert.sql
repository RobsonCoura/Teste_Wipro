USE db_locadora;

INSERT INTO tb_Cliente VALUES ('Rodrigo Gallego', '123.564.165-11', '2001-11-01');

INSERT INTO tb_Filme VALUES ('Efeito Cascata', '2013-02-05', 0),
							('A Bela e a Fera', '2017-03-16', 0),
							('Fantastico', '2011-08-22', 0),
							('Scooby', '2011-05-01', 1);

-- Adicionando uma locação com data de entrega após dois dias da data atual
INSERT INTO tb_Locacao VALUES (4, 1, DATEADD(day, 2 , GETDATE()));

-- Atualizando a data de entrega em 4 dias após a data atual
UPDATE tb_Locacao SET dtEntrega = DATEADD(day, 4 ,GETDATE()) WHERE idLocacao = 1;