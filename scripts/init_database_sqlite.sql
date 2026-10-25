BEGIN TRANSACTION;
CREATE TABLE "Courses" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Courses" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "ShortName" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "Modality" TEXT NOT NULL,
    "WorkloadHours" INTEGER NOT NULL,
    "TargetAudience" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    "ImageUrl" TEXT NOT NULL,
    "VideoUrl" TEXT NOT NULL,
    "AvailableDate" TEXT NULL,
    "Certification" TEXT NOT NULL
);
INSERT INTO "Courses" VALUES(1,'Matemática para a Precisão: Medidas e Escalas na Metalurgia','Matemática para Precisão','Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.','Matemática','Online',40,'Operadores de usinagem, torneiros mecânicos, caldeireiros e inspetores de qualidade industrial.','Inscrições Abertas','/assets/images/banner-ava.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-01-05 00:00:00','SESI');
INSERT INTO "Courses" VALUES(2,'Geometria e Modelagem no Design Têxtil','Modelagem no Design Têxtil','Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas, capacitando o aluno para o desenvolvimento de padrões industriais com alta eficiência e redução de desperdício de matéria-prima.','Matemática','Online',60,'Modelistas, designers de vestuário, técnicos têxteis e profissionais de confecção.','Inscrições Abertas','/assets/images/hero-image.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-02-01 00:00:00','SESI');
INSERT INTO "Courses" VALUES(3,'Estatística e Controle de Qualidade na Produção','Controle Estatístico de Processo','Leitura e interpretação de dados de controle de qualidade e produtividade. Capacita os alunos a aplicar CEP (Controle Estatístico de Processo), diagramas de dispersão, histogramas e cartas de controle em linhas de manufatura.','Matemática','Semipresencial',80,'Analistas de qualidade, supervisores de produção, engenheiros de processos e técnicos industriais.','Em Breve','/assets/images/video-thumb.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-03-10 00:00:00','SESI');
INSERT INTO "Courses" VALUES(4,'Física do Movimento e da Energia na Metalurgia','Física na Metalurgia','Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas. Aborda transformações termodinâmicas, mecânica dos fluidos aplicada à hidráulica e rendimento de motores industriais.','Ciências da Natureza','Presencial',60,'Mecânicos industriais, operadores de forno, técnicos de manutenção mecânica e caldeireiros.','Disponível','/assets/images/banner-ava.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2025-11-15 00:00:00','SESI');
INSERT INTO "Courses" VALUES(5,'Química e Sustentabilidade em Processos Industriais','Química e Sustentabilidade','Estudo das reações químicas envolvidas em tratamentos de superfícies, acabamento, gestão de efluentes industriais e práticas limpas para mitigação de impactos ambientais na indústria.','Sustentabilidade','Online',40,'Técnicos em meio ambiente, químicos industriais, operadores de tratamento de efluentes e gestores ESG.','Inscrições Abertas','/assets/images/hero-image.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-01-20 00:00:00','SESI');
INSERT INTO "Courses" VALUES(6,'Comunicação Assertiva e Redação Técnica para a Indústria','Comunicação e Redação Técnica','Desenvolva relatórios técnicos claros, ordens de serviço precisas e comunicação interpessoal eficaz para o chão de fábrica e reuniões executivas, alinhando produtividade e segurança.','Linguagens','Online',20,'Líderes de equipe, técnicos de segurança, analistas operacionais e supervisores de linha.','Disponível','/assets/images/video-thumb.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2025-10-01 00:00:00','SESI');
INSERT INTO "Courses" VALUES(7,'Eficiência Energética e Gestão de Utilidades Industriais','Eficiência Energética','Técnicas modernas para identificação de desperdícios de energia elétrica, vapor e ar comprimido. Elaboração de diagnósticos energéticos e implementação de soluções de baixo custo e alto retorno.','Sustentabilidade','Semipresencial',60,'Eletricistas industriais, gestores de manutenção e engenheiros de utilidades.','Em Breve','/assets/images/banner-ava.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-04-15 00:00:00','SESI');
INSERT INTO "Courses" VALUES(8,'Leitura e Interpretação de Desenho Técnico Mecânico','Desenho Técnico Mecânico','Domine a interpretação de vistas ortogonais, cortes, seções, rugosidade e tolerâncias geométricas (GD&T) segundo as normas ABNT e ISO vigentes.','Ciências da Natureza','Online',40,'Montadores mecânicos, soldadores, torneiros e estudantes de cursos industriais.','Inscrições Abertas','/assets/images/hero-image.png','https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ','2026-01-10 00:00:00','SESI');
CREATE TABLE "Faqs" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Faqs" PRIMARY KEY AUTOINCREMENT,
    "CourseId" INTEGER NOT NULL,
    "Question" TEXT NOT NULL,
    "Answer" TEXT NOT NULL,
    "Order" INTEGER NOT NULL,
    CONSTRAINT "FK_Faqs_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE
);
INSERT INTO "Faqs" VALUES(1,1,'Preciso ter conhecimento avançado prévio em matemática?','Não. O curso inicia pelos conceitos essenciais e avança progressivamente para a aplicação prática na rotina da metalurgia.',1);
INSERT INTO "Faqs" VALUES(2,1,'O certificado é reconhecido pelo mercado industrial?','Sim, o certificado emitido pelo SESI São Paulo possui reconhecimento nacional pela excelência técnica e alinhamento com a indústria.',2);
INSERT INTO "Faqs" VALUES(3,1,'Como funcionam as aulas e exercícios no AVA?','O conteúdo é 100% online com videoaulas dinâmicas, simulações interativas de instrumentos e avaliações práticas ao final de cada módulo.',3);
INSERT INTO "Faqs" VALUES(4,2,'O curso ensina softwares de modelagem digital?','Sim, abordamos tanto os fundamentos manuais geométricos quanto noções de softwares CAD para indústria de vestuário.',1);
INSERT INTO "Faqs" VALUES(5,3,'Como funcionam as aulas semipresenciais?','As aulas teóricas e exercícios são realizados online, e as oficinas práticas de medição acontecem nas unidades do SESI-SP.',1);
INSERT INTO "Faqs" VALUES(6,4,'Há aulas em laboratório com equipamentos reais?','Sim, o curso conta com bancadas didáticas industriais e fornos laboratoriais para ensaios térmicos.',1);
INSERT INTO "Faqs" VALUES(7,5,'O curso aborda a legislação ambiental brasileira?','Sim, todas as exigências das normas CONAMA e diretrizes de auditoria ISO 14001 são apresentadas no conteúdo.',1);
INSERT INTO "Faqs" VALUES(8,6,'O curso inclui modelos prontos de relatórios técnicos?','Sim, disponibilizamos templates para download compatíveis com os padrões mais adotados na indústria.',1);
INSERT INTO "Faqs" VALUES(9,7,'Qual o impacto real esperado após a conclusão do curso?','Os alunos desenvolvem um plano de ação prático capaz de gerar economia média de 8% a 15% nos custos de utilidades da empresa.',1);
INSERT INTO "Faqs" VALUES(10,8,'São necessários instrumentos manuais de desenho como compasso e prancheta?','Não, o foco é leitura e interpretação de desenhos de engenharia fornecidos digitalmente em exercícios práticos.',1);
CREATE TABLE "Lessons" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Lessons" PRIMARY KEY AUTOINCREMENT,
    "ModuleId" INTEGER NOT NULL,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "DurationMinutes" INTEGER NOT NULL,
    "Order" INTEGER NOT NULL,
    CONSTRAINT "FK_Lessons_Modules_ModuleId" FOREIGN KEY ("ModuleId") REFERENCES "Modules" ("Id") ON DELETE CASCADE
);
INSERT INTO "Lessons" VALUES(1,1,'1.1 Introdução à Metrologia Industrial','Conceitos de exatidão, precisão, rastreabilidade e tolerâncias dimensionais.',45,1);
INSERT INTO "Lessons" VALUES(2,1,'1.2 Boas Práticas e Segurança na Medição','Condições ambientais de medição, temperatura de referência e conservação de instrumentos.',50,2);
INSERT INTO "Lessons" VALUES(3,1,'1.3 Extras e Leituras Recomendadas','Apostilas técnicas e normas ABNT aplicáveis à metalurgia.',30,3);
INSERT INTO "Lessons" VALUES(4,2,'2.1 Leitura com Escala Nônio em Milímetros e Polegadas','Métodos de leitura e exercícios práticos no paquímetro universal.',60,1);
INSERT INTO "Lessons" VALUES(5,2,'2.2 Uso do Micrômetro em Peças de Alta Exigência','Princípio do fuso micrométrico, sensibilidade e calibração periódica.',55,2);
INSERT INTO "Lessons" VALUES(6,3,'3.1 Aplicação do Teorema de Pitágoras e Trigonometria','Cálculo de ângulos de corte, cones e cordas em furações circulares.',70,1);
INSERT INTO "Lessons" VALUES(7,3,'3.2 Sistema ISO de Tolerâncias e Ajustes (Furos e Eixos)','Tabelas de ajustes com folga, interferência e transição.',60,2);
INSERT INTO "Lessons" VALUES(8,4,'1.1 Formas Geométricas e Proporção Áurea','Estudo das formas na estética têxtil e aproveitamento do tecido.',45,1);
INSERT INTO "Lessons" VALUES(9,4,'1.2 Simetrias e Repetições Modulares','Criação de rapport contínuo e repetições técnicas.',50,2);
INSERT INTO "Lessons" VALUES(10,5,'2.1 Técnicas de Draping e Planificação','Transferência da volumetria anatômica para matrizes planas.',60,1);
INSERT INTO "Lessons" VALUES(11,5,'2.2 Otimização de Encaixe e Redução de Retalhos','Algoritmos geométricos para aproveitamento máximo da trama.',55,2);
INSERT INTO "Lessons" VALUES(12,6,'1.1 Média, Mediana, Moda e Desvio Padrão','Interpretação da variabilidade de processos fabris.',50,1);
INSERT INTO "Lessons" VALUES(13,6,'1.2 Construção e Análise de Histogramas','Identificação de distribuição normal e desvios de processo.',55,2);
INSERT INTO "Lessons" VALUES(14,7,'2.1 Cartas de Controle por Variáveis e Atributos','Detecção de causas especiais e instabilidade operacional.',65,1);
INSERT INTO "Lessons" VALUES(15,7,'2.2 Índices de Capacidade Cp e Cpk','Quantificação da capacidade em atender especificações de clientes.',60,2);
INSERT INTO "Lessons" VALUES(16,8,'1.1 Condução, Convecção e Radiação','Mecanismos térmicos aplicados a fornos de fundição e têmpera.',50,1);
INSERT INTO "Lessons" VALUES(17,8,'1.2 Dilatação Térmica e Comportamento dos Metais','Comportamento estrutural e coeficiente de expansão linear.',55,2);
INSERT INTO "Lessons" VALUES(18,9,'2.1 Torque, Potência e Rendimento Mecânico','Dimensionamento de rotações e forças de estampagem.',60,1);
INSERT INTO "Lessons" VALUES(19,10,'1.1 Os 12 Princípios da Química Verde','Economia atômica e substituição de reagentes tóxicos.',45,1);
INSERT INTO "Lessons" VALUES(20,10,'1.2 Tratamento e Reúso de Efluentes Industriais','Processos físico-químicos e biológicos de depuração.',50,2);
INSERT INTO "Lessons" VALUES(21,11,'2.1 Logística Reversa e Coprocessamento','Destinação nobre de borras, escórias e subprodutos.',60,1);
INSERT INTO "Lessons" VALUES(22,12,'1.1 Clareza, Objetividade e Padronização','Técnicas de escrita para documentação de não-conformidades e manutenções.',40,1);
INSERT INTO "Lessons" VALUES(23,12,'1.2 Feedback e Comunicação Não-Violenta no Trabalho','Como transmitir orientações operacionais sem ruídos.',45,2);
INSERT INTO "Lessons" VALUES(24,13,'1.1 Motores de Alto Rendimento e Inversores','Curvas de carga e redução de consumo elétrico.',50,1);
INSERT INTO "Lessons" VALUES(25,13,'1.2 Eliminação de Vazamentos em Redes Pneumáticas','Auditoria e monitoramento ultrassônico de perdas.',45,2);
INSERT INTO "Lessons" VALUES(26,14,'1.1 Primeiro e Terceiro Diedro','Diferenças entre convenções internacionais e ABNT.',45,1);
INSERT INTO "Lessons" VALUES(27,14,'1.2 Cortes Totais, Meio-Corte e Hachuras','Representação interna de componentes usinados.',50,2);
CREATE TABLE "Modules" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Modules" PRIMARY KEY AUTOINCREMENT,
    "CourseId" INTEGER NOT NULL,
    "Title" TEXT NOT NULL,
    "Order" INTEGER NOT NULL,
    CONSTRAINT "FK_Modules_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE
);
INSERT INTO "Modules" VALUES(1,1,'Módulo 1: Introdução à Metrologia e Normas Técnicas',1);
INSERT INTO "Modules" VALUES(2,1,'Módulo 2: Instrumentos de Medição Direta (Paquímetro e Micrômetro)',2);
INSERT INTO "Modules" VALUES(3,1,'Módulo 3: Cálculos de Tolerâncias, Ajustes e Trigonometria na Oficina',3);
INSERT INTO "Modules" VALUES(4,2,'Módulo 1: Fundamentos da Geometria Plana Aplicada ao Corte',1);
INSERT INTO "Modules" VALUES(5,2,'Módulo 2: Modelagem Tridimensional e Planificação',2);
INSERT INTO "Modules" VALUES(6,3,'Módulo 1: Fundamentos de Estatística Descritiva',1);
INSERT INTO "Modules" VALUES(7,3,'Módulo 2: Cartas de Controle e Capacidade de Processo (Cp e Cpk)',2);
INSERT INTO "Modules" VALUES(8,4,'Módulo 1: Termodinâmica e Transferência de Calor em Fornos',1);
INSERT INTO "Modules" VALUES(9,4,'Módulo 2: Cinemática e Dinâmica em Prensas e Tornos',2);
INSERT INTO "Modules" VALUES(10,5,'Módulo 1: Química Verde e Eficiência de Recursos',1);
INSERT INTO "Modules" VALUES(11,5,'Módulo 2: Economia Circular e Gestão de Resíduos Sólidos',2);
INSERT INTO "Modules" VALUES(12,6,'Módulo 1: Estruturação de Relatórios Técnicos e OS',1);
INSERT INTO "Modules" VALUES(13,7,'Módulo 1: Sistemas Motrizes e Ar Comprimido',1);
INSERT INTO "Modules" VALUES(14,8,'Módulo 1: Projeções Ortogonais e Vistas Básicas',1);
CREATE INDEX "IX_Courses_Category" ON "Courses" ("Category");
CREATE INDEX "IX_Courses_Modality" ON "Courses" ("Modality");
CREATE INDEX "IX_Courses_Status" ON "Courses" ("Status");
CREATE INDEX "IX_Faqs_CourseId" ON "Faqs" ("CourseId");
CREATE INDEX "IX_Lessons_ModuleId" ON "Lessons" ("ModuleId");
CREATE INDEX "IX_Modules_CourseId" ON "Modules" ("CourseId");
DELETE FROM "sqlite_sequence";
INSERT INTO "sqlite_sequence" VALUES('Courses',8);
INSERT INTO "sqlite_sequence" VALUES('Faqs',10);
INSERT INTO "sqlite_sequence" VALUES('Modules',14);
INSERT INTO "sqlite_sequence" VALUES('Lessons',27);
COMMIT;