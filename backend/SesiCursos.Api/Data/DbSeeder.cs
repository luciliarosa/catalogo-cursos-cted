using SesiCursos.Api.Models;

namespace SesiCursos.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Courses.Any())
        {
            return;
        }

        var courses = new List<Course>
        {
            new Course
            {
                Id = 1,
                FullName = "Matemática para a Precisão: Medidas e Escalas na Metalurgia",
                ShortName = "Matemática para Precisão",
                Description = "Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.",
                Category = "Matemática",
                Modality = "Online",
                WorkloadHours = 40,
                TargetAudience = "Operadores de usinagem, torneiros mecânicos, caldeireiros e inspetores de qualidade industrial.",
                Status = "Inscrições Abertas",
                ImageUrl = "/assets/images/banner-ava.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 1, 5),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Introdução à Metrologia e Normas Técnicas",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Introdução à Metrologia Industrial", Description = "Conceitos de exatidão, precisão, rastreabilidade e tolerâncias dimensionais.", DurationMinutes = 45, Order = 1 },
                            new CourseLesson { Title = "1.2 Boas Práticas e Segurança na Medição", Description = "Condições ambientais de medição, temperatura de referência e conservação de instrumentos.", DurationMinutes = 50, Order = 2 },
                            new CourseLesson { Title = "1.3 Extras e Leituras Recomendadas", Description = "Apostilas técnicas e normas ABNT aplicáveis à metalurgia.", DurationMinutes = 30, Order = 3 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 2: Instrumentos de Medição Direta (Paquímetro e Micrômetro)",
                        Order = 2,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "2.1 Leitura com Escala Nônio em Milímetros e Polegadas", Description = "Métodos de leitura e exercícios práticos no paquímetro universal.", DurationMinutes = 60, Order = 1 },
                            new CourseLesson { Title = "2.2 Uso do Micrômetro em Peças de Alta Exigência", Description = "Princípio do fuso micrométrico, sensibilidade e calibração periódica.", DurationMinutes = 55, Order = 2 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 3: Cálculos de Tolerâncias, Ajustes e Trigonometria na Oficina",
                        Order = 3,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "3.1 Aplicação do Teorema de Pitágoras e Trigonometria", Description = "Cálculo de ângulos de corte, cones e cordas em furações circulares.", DurationMinutes = 70, Order = 1 },
                            new CourseLesson { Title = "3.2 Sistema ISO de Tolerâncias e Ajustes (Furos e Eixos)", Description = "Tabelas de ajustes com folga, interferência e transição.", DurationMinutes = 60, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "Preciso ter conhecimento avançado prévio em matemática?",
                        Answer = "Não. O curso inicia pelos conceitos essenciais e avança progressivamente para a aplicação prática na rotina da metalurgia.",
                        Order = 1
                    },
                    new CourseFaq
                    {
                        Question = "O certificado é reconhecido pelo mercado industrial?",
                        Answer = "Sim, o certificado emitido pelo SESI São Paulo possui reconhecimento nacional pela excelência técnica e alinhamento com a indústria.",
                        Order = 2
                    },
                    new CourseFaq
                    {
                        Question = "Como funcionam as aulas e exercícios no AVA?",
                        Answer = "O conteúdo é 100% online com videoaulas dinâmicas, simulações interativas de instrumentos e avaliações práticas ao final de cada módulo.",
                        Order = 3
                    }
                }
            },
            new Course
            {
                Id = 2,
                FullName = "Geometria e Modelagem no Design Têxtil",
                ShortName = "Modelagem no Design Têxtil",
                Description = "Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas, capacitando o aluno para o desenvolvimento de padrões industriais com alta eficiência e redução de desperdício de matéria-prima.",
                Category = "Matemática",
                Modality = "Online",
                WorkloadHours = 60,
                TargetAudience = "Modelistas, designers de vestuário, técnicos têxteis e profissionais de confecção.",
                Status = "Inscrições Abertas",
                ImageUrl = "/assets/images/hero-image.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 2, 1),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Fundamentos da Geometria Plana Aplicada ao Corte",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Formas Geométricas e Proporção Áurea", Description = "Estudo das formas na estética têxtil e aproveitamento do tecido.", DurationMinutes = 45, Order = 1 },
                            new CourseLesson { Title = "1.2 Simetrias e Repetições Modulares", Description = "Criação de rapport contínuo e repetições técnicas.", DurationMinutes = 50, Order = 2 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 2: Modelagem Tridimensional e Planificação",
                        Order = 2,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "2.1 Técnicas de Draping e Planificação", Description = "Transferência da volumetria anatômica para matrizes planas.", DurationMinutes = 60, Order = 1 },
                            new CourseLesson { Title = "2.2 Otimização de Encaixe e Redução de Retalhos", Description = "Algoritmos geométricos para aproveitamento máximo da trama.", DurationMinutes = 55, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "O curso ensina softwares de modelagem digital?",
                        Answer = "Sim, abordamos tanto os fundamentos manuais geométricos quanto noções de softwares CAD para indústria de vestuário.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 3,
                FullName = "Estatística e Controle de Qualidade na Produção",
                ShortName = "Controle Estatístico de Processo",
                Description = "Leitura e interpretação de dados de controle de qualidade e produtividade. Capacita os alunos a aplicar CEP (Controle Estatístico de Processo), diagramas de dispersão, histogramas e cartas de controle em linhas de manufatura.",
                Category = "Matemática",
                Modality = "Semipresencial",
                WorkloadHours = 80,
                TargetAudience = "Analistas de qualidade, supervisores de produção, engenheiros de processos e técnicos industriais.",
                Status = "Em Breve",
                ImageUrl = "/assets/images/video-thumb.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 3, 10),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Fundamentos de Estatística Descritiva",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Média, Mediana, Moda e Desvio Padrão", Description = "Interpretação da variabilidade de processos fabris.", DurationMinutes = 50, Order = 1 },
                            new CourseLesson { Title = "1.2 Construção e Análise de Histogramas", Description = "Identificação de distribuição normal e desvios de processo.", DurationMinutes = 55, Order = 2 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 2: Cartas de Controle e Capacidade de Processo (Cp e Cpk)",
                        Order = 2,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "2.1 Cartas de Controle por Variáveis e Atributos", Description = "Detecção de causas especiais e instabilidade operacional.", DurationMinutes = 65, Order = 1 },
                            new CourseLesson { Title = "2.2 Índices de Capacidade Cp e Cpk", Description = "Quantificação da capacidade em atender especificações de clientes.", DurationMinutes = 60, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "Como funcionam as aulas semipresenciais?",
                        Answer = "As aulas teóricas e exercícios são realizados online, e as oficinas práticas de medição acontecem nas unidades do SESI-SP.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 4,
                FullName = "Física do Movimento e da Energia na Metalurgia",
                ShortName = "Física na Metalurgia",
                Description = "Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas. Aborda transformações termodinâmicas, mecânica dos fluidos aplicada à hidráulica e rendimento de motores industriais.",
                Category = "Ciências da Natureza",
                Modality = "Presencial",
                WorkloadHours = 60,
                TargetAudience = "Mecânicos industriais, operadores de forno, técnicos de manutenção mecânica e caldeireiros.",
                Status = "Disponível",
                ImageUrl = "/assets/images/banner-ava.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2025, 11, 15),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Termodinâmica e Transferência de Calor em Fornos",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Condução, Convecção e Radiação", Description = "Mecanismos térmicos aplicados a fornos de fundição e têmpera.", DurationMinutes = 50, Order = 1 },
                            new CourseLesson { Title = "1.2 Dilatação Térmica e Comportamento dos Metais", Description = "Comportamento estrutural e coeficiente de expansão linear.", DurationMinutes = 55, Order = 2 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 2: Cinemática e Dinâmica em Prensas e Tornos",
                        Order = 2,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "2.1 Torque, Potência e Rendimento Mecânico", Description = "Dimensionamento de rotações e forças de estampagem.", DurationMinutes = 60, Order = 1 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "Há aulas em laboratório com equipamentos reais?",
                        Answer = "Sim, o curso conta com bancadas didáticas industriais e fornos laboratoriais para ensaios térmicos.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 5,
                FullName = "Química e Sustentabilidade em Processos Industriais",
                ShortName = "Química e Sustentabilidade",
                Description = "Estudo das reações químicas envolvidas em tratamentos de superfícies, acabamento, gestão de efluentes industriais e práticas limpas para mitigação de impactos ambientais na indústria.",
                Category = "Sustentabilidade",
                Modality = "Online",
                WorkloadHours = 40,
                TargetAudience = "Técnicos em meio ambiente, químicos industriais, operadores de tratamento de efluentes e gestores ESG.",
                Status = "Inscrições Abertas",
                ImageUrl = "/assets/images/hero-image.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 1, 20),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Química Verde e Eficiência de Recursos",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Os 12 Princípios da Química Verde", Description = "Economia atômica e substituição de reagentes tóxicos.", DurationMinutes = 45, Order = 1 },
                            new CourseLesson { Title = "1.2 Tratamento e Reúso de Efluentes Industriais", Description = "Processos físico-químicos e biológicos de depuração.", DurationMinutes = 50, Order = 2 }
                        }
                    },
                    new CourseModule
                    {
                        Title = "Módulo 2: Economia Circular e Gestão de Resíduos Sólidos",
                        Order = 2,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "2.1 Logística Reversa e Coprocessamento", Description = "Destinação nobre de borras, escórias e subprodutos.", DurationMinutes = 60, Order = 1 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "O curso aborda a legislação ambiental brasileira?",
                        Answer = "Sim, todas as exigências das normas CONAMA e diretrizes de auditoria ISO 14001 são apresentadas no conteúdo.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 6,
                FullName = "Comunicação Assertiva e Redação Técnica para a Indústria",
                ShortName = "Comunicação e Redação Técnica",
                Description = "Desenvolva relatórios técnicos claros, ordens de serviço precisas e comunicação interpessoal eficaz para o chão de fábrica e reuniões executivas, alinhando produtividade e segurança.",
                Category = "Linguagens",
                Modality = "Online",
                WorkloadHours = 20,
                TargetAudience = "Líderes de equipe, técnicos de segurança, analistas operacionais e supervisores de linha.",
                Status = "Disponível",
                ImageUrl = "/assets/images/video-thumb.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2025, 10, 1),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Estruturação de Relatórios Técnicos e OS",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Clareza, Objetividade e Padronização", Description = "Técnicas de escrita para documentação de não-conformidades e manutenções.", DurationMinutes = 40, Order = 1 },
                            new CourseLesson { Title = "1.2 Feedback e Comunicação Não-Violenta no Trabalho", Description = "Como transmitir orientações operacionais sem ruídos.", DurationMinutes = 45, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "O curso inclui modelos prontos de relatórios técnicos?",
                        Answer = "Sim, disponibilizamos templates para download compatíveis com os padrões mais adotados na indústria.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 7,
                FullName = "Eficiência Energética e Gestão de Utilidades Industriais",
                ShortName = "Eficiência Energética",
                Description = "Técnicas modernas para identificação de desperdícios de energia elétrica, vapor e ar comprimido. Elaboração de diagnósticos energéticos e implementação de soluções de baixo custo e alto retorno.",
                Category = "Sustentabilidade",
                Modality = "Semipresencial",
                WorkloadHours = 60,
                TargetAudience = "Eletricistas industriais, gestores de manutenção e engenheiros de utilidades.",
                Status = "Em Breve",
                ImageUrl = "/assets/images/banner-ava.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 4, 15),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Sistemas Motrizes e Ar Comprimido",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Motores de Alto Rendimento e Inversores", Description = "Curvas de carga e redução de consumo elétrico.", DurationMinutes = 50, Order = 1 },
                            new CourseLesson { Title = "1.2 Eliminação de Vazamentos em Redes Pneumáticas", Description = "Auditoria e monitoramento ultrassônico de perdas.", DurationMinutes = 45, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "Qual o impacto real esperado após a conclusão do curso?",
                        Answer = "Os alunos desenvolvem um plano de ação prático capaz de gerar economia média de 8% a 15% nos custos de utilidades da empresa.",
                        Order = 1
                    }
                }
            },
            new Course
            {
                Id = 8,
                FullName = "Leitura e Interpretação de Desenho Técnico Mecânico",
                ShortName = "Desenho Técnico Mecânico",
                Description = "Domine a interpretação de vistas ortogonais, cortes, seções, rugosidade e tolerâncias geométricas (GD&T) segundo as normas ABNT e ISO vigentes.",
                Category = "Ciências da Natureza",
                Modality = "Online",
                WorkloadHours = 40,
                TargetAudience = "Montadores mecânicos, soldadores, torneiros e estudantes de cursos industriais.",
                Status = "Inscrições Abertas",
                ImageUrl = "/assets/images/hero-image.png",
                VideoUrl = "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
                AvailableDate = new DateTime(2026, 1, 10),
                Certification = "SESI",
                Modules = new List<CourseModule>
                {
                    new CourseModule
                    {
                        Title = "Módulo 1: Projeções Ortogonais e Vistas Básicas",
                        Order = 1,
                        Lessons = new List<CourseLesson>
                        {
                            new CourseLesson { Title = "1.1 Primeiro e Terceiro Diedro", Description = "Diferenças entre convenções internacionais e ABNT.", DurationMinutes = 45, Order = 1 },
                            new CourseLesson { Title = "1.2 Cortes Totais, Meio-Corte e Hachuras", Description = "Representação interna de componentes usinados.", DurationMinutes = 50, Order = 2 }
                        }
                    }
                },
                Faqs = new List<CourseFaq>
                {
                    new CourseFaq
                    {
                        Question = "São necessários instrumentos manuais de desenho como compasso e prancheta?",
                        Answer = "Não, o foco é leitura e interpretação de desenhos de engenharia fornecidos digitalmente em exercícios práticos.",
                        Order = 1
                    }
                }
            }
        };

        context.Courses.AddRange(courses);
        context.SaveChanges();
    }
}
