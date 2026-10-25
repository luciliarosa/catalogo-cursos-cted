-- ========================================================
-- BANCO DE DADOS: Catálogo de Cursos CTED SESI-SP
-- Provedor: PostgreSQL
-- Autor: Lucilia Rosa
-- Data: 22/10/2026 - 25/10/2026
-- Descrição: Script de criação do banco de dados | Processo seletivo - Desenvolvedor de Sistemas I - 519-2026
-- ========================================================

CREATE TABLE IF NOT EXISTS "Courses" (
    "Id" SERIAL PRIMARY KEY,
    "FullName" VARCHAR(250) NOT NULL,
    "ShortName" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(2000) NOT NULL,
    "Category" VARCHAR(100) NOT NULL,
    "Modality" VARCHAR(50) NOT NULL,
    "WorkloadHours" INTEGER NOT NULL,
    "TargetAudience" VARCHAR(300) NOT NULL,
    "Status" VARCHAR(50) NOT NULL,
    "ImageUrl" VARCHAR(500) NOT NULL,
    "VideoUrl" VARCHAR(500) NOT NULL,
    "AvailableDate" TIMESTAMP WITH TIME ZONE NULL,
    "Certification" VARCHAR(100) NOT NULL DEFAULT 'SESI'
);

CREATE INDEX IF NOT EXISTS "IX_Courses_Category" ON "Courses" ("Category");
CREATE INDEX IF NOT EXISTS "IX_Courses_Modality" ON "Courses" ("Modality");
CREATE INDEX IF NOT EXISTS "IX_Courses_Status" ON "Courses" ("Status");

CREATE TABLE IF NOT EXISTS "Modules" (
    "Id" SERIAL PRIMARY KEY,
    "CourseId" INTEGER NOT NULL REFERENCES "Courses"("Id") ON DELETE CASCADE,
    "Title" VARCHAR(200) NOT NULL,
    "Order" INTEGER NOT NULL
);

CREATE INDEX IF NOT EXISTS "IX_Modules_CourseId" ON "Modules" ("CourseId");

CREATE TABLE IF NOT EXISTS "Lessons" (
    "Id" SERIAL PRIMARY KEY,
    "ModuleId" INTEGER NOT NULL REFERENCES "Modules"("Id") ON DELETE CASCADE,
    "Title" VARCHAR(200) NOT NULL,
    "Description" VARCHAR(1000) NOT NULL,
    "DurationMinutes" INTEGER NOT NULL,
    "Order" INTEGER NOT NULL
);

CREATE INDEX IF NOT EXISTS "IX_Lessons_ModuleId" ON "Lessons" ("ModuleId");

CREATE TABLE IF NOT EXISTS "Faqs" (
    "Id" SERIAL PRIMARY KEY,
    "CourseId" INTEGER NOT NULL REFERENCES "Courses"("Id") ON DELETE CASCADE,
    "Question" VARCHAR(300) NOT NULL,
    "Answer" VARCHAR(1500) NOT NULL,
    "Order" INTEGER NOT NULL
);

CREATE INDEX IF NOT EXISTS "IX_Faqs_CourseId" ON "Faqs" ("CourseId");

-- ========================================================
-- SEED DATA: Cursos Fictícios Iniciais
-- ========================================================

INSERT INTO "Courses" ("Id", "FullName", "ShortName", "Description", "Category", "Modality", "WorkloadHours", "TargetAudience", "Status", "ImageUrl", "VideoUrl", "AvailableDate", "Certification")
VALUES 
(1, 'Matemática para a Precisão: Medidas e Escalas na Metalurgia', 'Matemática para Precisão', 'Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.', 'Matemática', 'Online', 40, 'Operadores de usinagem, torneiros mecânicos, caldeireiros e inspetores de qualidade industrial.', 'Inscrições Abertas', '/assets/images/banner-ava.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-01-05T00:00:00Z', 'SESI'),
(2, 'Geometria e Modelagem no Design Têxtil', 'Modelagem no Design Têxtil', 'Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas, capacitando o aluno para o desenvolvimento de padrões industriais com alta eficiência e redução de desperdício de matéria-prima.', 'Matemática', 'Online', 60, 'Modelistas, designers de vestuário, técnicos têxteis e profissionais de confecção.', 'Inscrições Abertas', '/assets/images/hero-image.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-02-01T00:00:00Z', 'SESI'),
(3, 'Estatística e Controle de Qualidade na Produção', 'Controle Estatístico de Processo', 'Leitura e interpretação de dados de controle de qualidade e produtividade. Capacita os alunos a aplicar CEP (Controle Estatístico de Processo), diagramas de dispersão, histogramas e cartas de controle em linhas de manufatura.', 'Matemática', 'Semipresencial', 80, 'Analistas de qualidade, supervisores de produção, engenheiros de processos e técnicos industriais.', 'Em Breve', '/assets/images/video-thumb.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-03-10T00:00:00Z', 'SESI'),
(4, 'Física do Movimento e da Energia na Metalurgia', 'Física na Metalurgia', 'Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas. Aborda transformações termodinâmicas, mecânica dos fluidos aplicada à hidráulica e rendimento de motores industriais.', 'Ciências da Natureza', 'Presencial', 60, 'Mecânicos industriais, operadores de forno, técnicos de manutenção mecânica e caldeireiros.', 'Disponível', '/assets/images/banner-ava.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2025-11-15T00:00:00Z', 'SESI'),
(5, 'Química e Sustentabilidade em Processos Industriais', 'Química e Sustentabilidade', 'Estudo das reações químicas envolvidas em tratamentos de superfícies, acabamento, gestão de efluentes industriais e práticas limpas para mitigação de impactos ambientais na indústria.', 'Sustentabilidade', 'Online', 40, 'Técnicos em meio ambiente, químicos industriais, operadores de tratamento de efluentes e gestores ESG.', 'Inscrições Abertas', '/assets/images/hero-image.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-01-20T00:00:00Z', 'SESI'),
(6, 'Comunicação Assertiva e Redação Técnica para a Indústria', 'Comunicação e Redação Técnica', 'Desenvolva relatórios técnicos claros, ordens de serviço precisas e comunicação interpessoal eficaz para o chão de fábrica e reuniões executivas, alinhando produtividade e segurança.', 'Linguagens', 'Online', 20, 'Líderes de equipe, técnicos de segurança, analistas operacionais e supervisores de linha.', 'Disponível', '/assets/images/video-thumb.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2025-10-01T00:00:00Z', 'SESI'),
(7, 'Eficiência Energética e Gestão de Utilidades Industriais', 'Eficiência Energética', 'Técnicas modernas para identificação de desperdícios de energia elétrica, vapor e ar comprimido. Elaboração de diagnósticos energéticos e implementação de soluções de baixo custo e alto retorno.', 'Sustentabilidade', 'Semipresencial', 60, 'Eletricistas industriais, gestores de manutenção e engenheiros de utilidades.', 'Em Breve', '/assets/images/banner-ava.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-04-15T00:00:00Z', 'SESI'),
(8, 'Leitura e Interpretação de Desenho Técnico Mecânico', 'Desenho Técnico Mecânico', 'Domine a interpretação de vistas ortogonais, cortes, seções, rugosidade e tolerâncias geométricas (GD&T) segundo as normas ABNT e ISO vigentes.', 'Ciências da Natureza', 'Online', 40, 'Montadores mecânicos, soldadores, torneiros e estudantes de cursos industriais.', 'Inscrições Abertas', '/assets/images/hero-image.png', 'https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ', '2026-01-10T00:00:00Z', 'SESI')
ON CONFLICT ("Id") DO NOTHING;

-- Módulos
INSERT INTO "Modules" ("Id", "CourseId", "Title", "Order") VALUES
(1, 1, 'Módulo 1: Introdução à Metrologia e Normas Técnicas', 1),
(2, 1, 'Módulo 2: Instrumentos de Medição Direta (Paquímetro e Micrômetro)', 2),
(3, 1, 'Módulo 3: Cálculos de Tolerâncias, Ajustes e Trigonometria na Oficina', 3),
(4, 2, 'Módulo 1: Fundamentos da Geometria Plana Aplicada ao Corte', 1),
(5, 2, 'Módulo 2: Modelagem Tridimensional e Planificação', 2),
(6, 3, 'Módulo 1: Fundamentos de Estatística Descritiva', 1),
(7, 3, 'Módulo 2: Cartas de Controle e Capacidade de Processo (Cp e Cpk)', 2),
(8, 4, 'Módulo 1: Termodinâmica e Transferência de Calor em Fornos', 1),
(9, 4, 'Módulo 2: Cinemática e Dinâmica em Prensas e Tornos', 2),
(10, 5, 'Módulo 1: Química Verde e Eficiência de Recursos', 1),
(11, 5, 'Módulo 2: Economia Circular e Gestão de Resíduos Sólidos', 2),
(12, 6, 'Módulo 1: Estruturação de Relatórios Técnicos e OS', 1),
(13, 7, 'Módulo 1: Sistemas Motrizes e Ar Comprimido', 1),
(14, 8, 'Módulo 1: Projeções Ortogonais e Vistas Básicas', 1)
ON CONFLICT ("Id") DO NOTHING;

-- Aulas
INSERT INTO "Lessons" ("Id", "ModuleId", "Title", "Description", "DurationMinutes", "Order") VALUES
(1, 1, '1.1 Introdução à Metrologia Industrial', 'Conceitos de exatidão, precisão, rastreabilidade e tolerâncias dimensionais.', 45, 1),
(2, 1, '1.2 Boas Práticas e Segurança na Medição', 'Condições ambientais de medição, temperatura de referência e conservação de instrumentos.', 50, 2),
(3, 1, '1.3 Extras e Leituras Recomendadas', 'Apostilas técnicas e normas ABNT aplicáveis à metalurgia.', 30, 3),
(4, 2, '2.1 Leitura com Escala Nônio em Milímetros e Polegadas', 'Métodos de leitura e exercícios práticos no paquímetro universal.', 60, 1),
(5, 2, '2.2 Uso do Micrômetro em Peças de Alta Exigência', 'Princípio do fuso micrométrico, sensibilidade e calibração periódica.', 55, 2),
(6, 3, '3.1 Aplicação do Teorema de Pitágoras e Trigonometria', 'Cálculo de ângulos de corte, cones e cordas em furações circulares.', 70, 1),
(7, 3, '3.2 Sistema ISO de Tolerâncias e Ajustes (Furos e Eixos)', 'Tabelas de ajustes com folga, interferência e transição.', 60, 2)
ON CONFLICT ("Id") DO NOTHING;

-- FAQs
INSERT INTO "Faqs" ("Id", "CourseId", "Question", "Answer", "Order") VALUES
(1, 1, 'Preciso ter conhecimento avançado prévio em matemática?', 'Não. O curso inicia pelos conceitos essenciais e avança progressivamente para a aplicação prática na rotina da metalurgia.', 1),
(2, 1, 'O certificado é reconhecido pelo mercado industrial?', 'Sim, o certificado emitido pelo SESI São Paulo possui reconhecimento nacional pela excelência técnica e alinhamento com a indústria.', 2),
(3, 1, 'Como funcionam as aulas e exercícios no AVA?', 'O conteúdo é 100% online com videoaulas dinâmicas, simulações interativas de instrumentos e avaliações práticas ao final de cada módulo.', 3)
ON CONFLICT ("Id") DO NOTHING;
