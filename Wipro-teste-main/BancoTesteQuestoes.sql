--1
SELECT 
      s.dsStatus AS status, 
      COUNT(P.idProcesso) AS qtd 
FROM 
      tb_Processo p 
      INNER JOIN tb_Status s ON s.idStatus = p.idStatus 
WHERE 
      p.idStatus IN (SELECT idStatus FROM tb_Status) 
GROUP BY 
      s.dsStatus

		
--2 
SELECT
      idProcesso
FROM 
      tb_Andamento 
WHERE YEAR
      (dtAndamento) = '2013' 
ORDER BY 
      dtAndamento 
DESC


--3
SELECT 
      t1.* 
FROM (
     SELECT 
	     DISTINCT COUNT(p.dtEncerramento) AS qtd, 
	     p.dtEncerramento AS Data 
     FROM 
	     tb_Processo p 
     GROUP BY 
	     p.DtEncerramento
     ) AS t1 
     WHERE 
	     t1.qtd >= 5

--4
SELECT REPLICATE
    ('0', 12 - LEN(nroProcesso)) + 
    RTrim(nroProcesso) 
FROM 
    tb_Processo;
