// Define a URL da API na porta 5298
const API_BASE_URL = (window.location.port === '5298') ? '' : 'http://localhost:5298';

// Lista de reserva caso a página seja aberta sem o servidor da API estar rodando
const FALLBACK_COURSES = [
  {
    id: 1,
    fullName: "Matemática para a Precisão: Medidas e Escalas na Metalurgia",
    shortName: "Matemática para Precisão",
    description: "Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.",
    category: "Matemática",
    modality: "Online",
    workloadHours: 40,
    targetAudience: "Operadores de usinagem, torneiros mecânicos, caldeireiros e inspetores de qualidade industrial.",
    status: "Inscrições Abertas",
    imageUrl: "/assets/images/banner-ava.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-01-05T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 1,
        title: "Módulo 1: Introdução à Metrologia e Normas Técnicas",
        order: 1,
        lessons: [
          { id: 1, title: "1.1 Introdução à Metrologia Industrial", description: "Conceitos de exatidão, precisão, rastreabilidade e tolerâncias dimensionais.", durationMinutes: 45, order: 1 },
          { id: 2, title: "1.2 Boas Práticas e Segurança na Medição", description: "Condições ambientais de medição, temperatura de referência e conservação de instrumentos.", durationMinutes: 50, order: 2 },
          { id: 3, title: "1.3 Extras e Leituras Recomendadas", description: "Apostilas técnicas e normas ABNT aplicáveis à metalurgia.", durationMinutes: 30, order: 3 }
        ]
      },
      {
        id: 2,
        title: "Módulo 2: Instrumentos de Medição Direta (Paquímetro e Micrômetro)",
        order: 2,
        lessons: [
          { id: 4, title: "2.1 Leitura com Escala Nônio em Milímetros e Polegadas", description: "Métodos de leitura e exercícios práticos no paquímetro universal.", durationMinutes: 60, order: 1 },
          { id: 5, title: "2.2 Uso do Micrômetro em Peças de Alta Exigência", description: "Princípio do fuso micrométrico, sensibilidade e calibração periódica.", durationMinutes: 55, order: 2 }
        ]
      },
      {
        id: 3,
        title: "Módulo 3: Cálculos de Tolerâncias, Ajustes e Trigonometria na Oficina",
        order: 3,
        lessons: [
          { id: 6, title: "3.1 Aplicação do Teorema de Pitágoras e Trigonometria", description: "Cálculo de ângulos de corte, cones e cordas em furações circulares.", durationMinutes: 70, order: 1 },
          { id: 7, title: "3.2 Sistema ISO de Tolerâncias e Ajustes (Furos e Eixos)", description: "Tabelas de ajustes com folga, interferência e transição.", durationMinutes: 60, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 1, question: "Preciso ter conhecimento avançado prévio em matemática?", answer: "Não. O curso inicia pelos conceitos essenciais e avança progressivamente para a aplicação prática na rotina da metalurgia.", order: 1 },
      { id: 2, question: "O certificado é reconhecido pelo mercado industrial?", answer: "Sim, o certificado emitido pelo SESI São Paulo possui reconhecimento nacional pela excelência técnica e alinhamento com a indústria.", order: 2 },
      { id: 3, question: "Como funcionam as aulas e exercícios no AVA?", answer: "O conteúdo é 100% online com videoaulas dinâmicas, simulações interativas de instrumentos e avaliações práticas ao final de cada módulo.", order: 3 }
    ]
  },
  {
    id: 2,
    fullName: "Geometria e Modelagem no Design Têxtil",
    shortName: "Modelagem no Design Têxtil",
    description: "Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas, capacitando o aluno para o desenvolvimento de padrões industriais com alta eficiência e redução de desperdício de matéria-prima.",
    category: "Matemática",
    modality: "Online",
    workloadHours: 60,
    targetAudience: "Modelistas, designers de vestuário, técnicos têxteis e profissionais de confecção.",
    status: "Inscrições Abertas",
    imageUrl: "/assets/images/hero-image.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-02-01T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 4,
        title: "Módulo 1: Fundamentos da Geometria Plana Aplicada ao Corte",
        order: 1,
        lessons: [
          { id: 8, title: "1.1 Formas Geométricas e Proporção Áurea", description: "Estudo das formas na estética têxtil e aproveitamento do tecido.", durationMinutes: 45, order: 1 },
          { id: 9, title: "1.2 Simetrias e Repetições Modulares", description: "Criação de rapport contínuo e repetições técnicas.", durationMinutes: 50, order: 2 }
        ]
      },
      {
        id: 5,
        title: "Módulo 2: Modelagem Tridimensional e Planificação",
        order: 2,
        lessons: [
          { id: 10, title: "2.1 Técnicas de Draping e Planificação", description: "Transferência da volumetria anatômica para matrizes planas.", durationMinutes: 60, order: 1 },
          { id: 11, title: "2.2 Otimização de Encaixe e Redução de Retalhos", description: "Algoritmos geométricos para aproveitamento máximo da trama.", durationMinutes: 55, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 4, question: "O curso ensina softwares de modelagem digital?", answer: "Sim, abordamos tanto os fundamentos manuais geométricos quanto noções de softwares CAD para indústria de vestuário.", order: 1 }
    ]
  },
  {
    id: 3,
    fullName: "Estatística e Controle de Qualidade na Produção",
    shortName: "Controle Estatístico de Processo",
    description: "Leitura e interpretação de dados de controle de qualidade e produtividade. Capacita os alunos a aplicar CEP (Controle Estatístico de Processo), diagramas de dispersão, histogramas e cartas de controle em linhas de manufatura.",
    category: "Matemática",
    modality: "Semipresencial",
    workloadHours: 80,
    targetAudience: "Analistas de qualidade, supervisores de produção, engenheiros de processos e técnicos industriais.",
    status: "Em Breve",
    imageUrl: "/assets/images/video-thumb.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-03-10T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 6,
        title: "Módulo 1: Fundamentos de Estatística Descritiva",
        order: 1,
        lessons: [
          { id: 12, title: "1.1 Média, Mediana, Moda e Desvio Padrão", description: "Interpretação da variabilidade de processos fabris.", durationMinutes: 50, order: 1 },
          { id: 13, title: "1.2 Construção e Análise de Histogramas", description: "Identificação de distribuição normal e desvios de processo.", durationMinutes: 55, order: 2 }
        ]
      },
      {
        id: 7,
        title: "Módulo 2: Cartas de Controle e Capacidade de Processo (Cp e Cpk)",
        order: 2,
        lessons: [
          { id: 14, title: "2.1 Cartas de Controle por Variáveis e Atributos", description: "Detecção de causas especiais e instabilidade operacional.", durationMinutes: 65, order: 1 },
          { id: 15, title: "2.2 Índices de Capacidade Cp e Cpk", description: "Quantificação da capacidade em atender especificações de clientes.", durationMinutes: 60, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 5, question: "Como funcionam as aulas semipresenciais?", answer: "As aulas teóricas e exercícios são realizados online, e as oficinas práticas de medição acontecem nas unidades do SESI-SP.", order: 1 }
    ]
  },
  {
    id: 4,
    fullName: "Física do Movimento e da Energia na Metalurgia",
    shortName: "Física na Metalurgia",
    description: "Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas. Aborda transformações termodinâmicas, mecânica dos fluidos aplicada à hidráulica e rendimento de motores industriais.",
    category: "Ciências da Natureza",
    modality: "Presencial",
    workloadHours: 60,
    targetAudience: "Mecânicos industriais, operadores de forno, técnicos de manutenção mecânica e caldeireiros.",
    status: "Disponível",
    imageUrl: "/assets/images/banner-ava.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2025-11-15T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 8,
        title: "Módulo 1: Termodinâmica e Transferência de Calor em Fornos",
        order: 1,
        lessons: [
          { id: 16, title: "1.1 Condução, Convecção e Radiação", description: "Mecanismos térmicos aplicados a fornos de fundição e têmpera.", durationMinutes: 50, order: 1 },
          { id: 17, title: "1.2 Dilatação Térmica e Comportamento dos Metais", description: "Comportamento estrutural e coeficiente de expansão linear.", durationMinutes: 55, order: 2 }
        ]
      },
      {
        id: 9,
        title: "Módulo 2: Cinemática e Dinâmica em Prensas e Tornos",
        order: 2,
        lessons: [
          { id: 18, title: "2.1 Torque, Potência e Rendimento Mecânico", description: "Dimensionamento de rotações e forças de estampagem.", durationMinutes: 60, order: 1 }
        ]
      }
    ],
    faqs: [
      { id: 6, question: "Há aulas em laboratório com equipamentos reais?", answer: "Sim, o curso conta com bancadas didáticas industriais e fornos laboratoriais para ensaios térmicos.", order: 1 }
    ]
  },
  {
    id: 5,
    fullName: "Química e Sustentabilidade em Processos Industriais",
    shortName: "Química e Sustentabilidade",
    description: "Estudo das reações químicas envolvidas em tratamentos de superfícies, acabamento, gestão de efluentes industriais e práticas limpas para mitigação de impactos ambientais na indústria.",
    category: "Sustentabilidade",
    modality: "Online",
    workloadHours: 40,
    targetAudience: "Técnicos em meio ambiente, químicos industriais, operadores de tratamento de efluentes e gestores ESG.",
    status: "Inscrições Abertas",
    imageUrl: "/assets/images/hero-image.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-01-20T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 10,
        title: "Módulo 1: Química Verde e Eficiência de Recursos",
        order: 1,
        lessons: [
          { id: 19, title: "1.1 Os 12 Princípios da Química Verde", description: "Economia atômica e substituição de reagentes tóxicos.", durationMinutes: 45, order: 1 },
          { id: 20, title: "1.2 Tratamento e Reúso de Efluentes Industriais", description: "Processos físico-químicos e biológicos de depuração.", durationMinutes: 50, order: 2 }
        ]
      },
      {
        id: 11,
        title: "Módulo 2: Economia Circular e Gestão de Resíduos Sólidos",
        order: 2,
        lessons: [
          { id: 21, title: "2.1 Logística Reversa e Coprocessamento", description: "Destinação nobre de borras, escórias e subprodutos.", durationMinutes: 60, order: 1 }
        ]
      }
    ],
    faqs: [
      { id: 7, question: "O curso aborda a legislação ambiental brasileira?", answer: "Sim, todas as exigências das normas CONAMA e diretrizes de auditoria ISO 14001 são apresentadas no conteúdo.", order: 1 }
    ]
  },
  {
    id: 6,
    fullName: "Comunicação Assertiva e Redação Técnica para a Indústria",
    shortName: "Comunicação e Redação Técnica",
    description: "Desenvolva relatórios técnicos claros, ordens de serviço precisas e comunicação interpessoal eficaz para o chão de fábrica e reuniões executivas, alinhando produtividade e segurança.",
    category: "Linguagens",
    modality: "Online",
    workloadHours: 20,
    targetAudience: "Líderes de equipe, técnicos de segurança, analistas operacionais e supervisores de linha.",
    status: "Disponível",
    imageUrl: "/assets/images/video-thumb.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2025-10-01T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 12,
        title: "Módulo 1: Estruturação de Relatórios Técnicos e OS",
        order: 1,
        lessons: [
          { id: 22, title: "1.1 Clareza, Objetividade e Padronização", description: "Técnicas de escrita para documentação de não-conformidades e manutenções.", durationMinutes: 40, order: 1 },
          { id: 23, title: "1.2 Feedback e Comunicação Não-Violenta no Trabalho", description: "Como transmitir orientações operacionais sem ruídos.", durationMinutes: 45, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 8, question: "O curso inclui modelos prontos de relatórios técnicos?", answer: "Sim, disponibilizamos templates para download compatíveis com os padrões mais adotados na indústria.", order: 1 }
    ]
  },
  {
    id: 7,
    fullName: "Eficiência Energética e Gestão de Utilidades Industriais",
    shortName: "Eficiência Energética",
    description: "Técnicas modernas para identificação de desperdícios de energia elétrica, vapor e ar comprimido. Elaboração de diagnósticos energéticos e implementação de soluções de baixo custo e alto retorno.",
    category: "Sustentabilidade",
    modality: "Semipresencial",
    workloadHours: 60,
    targetAudience: "Eletricistas industriais, gestores de manutenção e engenheiros de utilidades.",
    status: "Em Breve",
    imageUrl: "/assets/images/banner-ava.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-04-15T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 13,
        title: "Módulo 1: Sistemas Motrizes e Ar Comprimido",
        order: 1,
        lessons: [
          { id: 24, title: "1.1 Motores de Alto Rendimento e Inversores", description: "Curvas de carga e redução de consumo elétrico.", durationMinutes: 50, order: 1 },
          { id: 25, title: "1.2 Eliminação de Vazamentos em Redes Pneumáticas", description: "Auditoria e monitoramento ultrassônico de perdas.", durationMinutes: 45, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 9, question: "Qual o impacto real esperado após a conclusão do curso?", answer: "Os alunos desenvolvem um plano de ação prático capaz de gerar economia média de 8% a 15% nos custos de utilidades da empresa.", order: 1 }
    ]
  },
  {
    id: 8,
    fullName: "Leitura e Interpretação de Desenho Técnico Mecânico",
    shortName: "Desenho Técnico Mecânico",
    description: "Domine a interpretação de vistas ortogonais, cortes, seções, rugosidade e tolerâncias geométricas (GD&T) segundo as normas ABNT e ISO vigentes.",
    category: "Ciências da Natureza",
    modality: "Online",
    workloadHours: 40,
    targetAudience: "Montadores mecânicos, soldadores, torneiros e estudantes de cursos industriais.",
    status: "Inscrições Abertas",
    imageUrl: "/assets/images/hero-image.png",
    videoUrl: "https://www.youtube-nocookie.com/embed/dQw4w9WgXcQ",
    availableDate: "2026-01-10T00:00:00",
    certification: "SESI",
    modules: [
      {
        id: 14,
        title: "Módulo 1: Projeções Ortogonais e Vistas Básicas",
        order: 1,
        lessons: [
          { id: 26, title: "1.1 Primeiro e Terceiro Diedro", description: "Diferenças entre convenções internacionais e ABNT.", durationMinutes: 45, order: 1 },
          { id: 27, title: "1.2 Cortes Totais, Meio-Corte e Hachuras", description: "Representação interna de componentes usinados.", durationMinutes: 50, order: 2 }
        ]
      }
    ],
    faqs: [
      { id: 10, question: "São necessários instrumentos manuais de desenho como compasso e prancheta?", answer: "Não, o foco é leitura e interpretação de desenhos de engenharia fornecidos digitalmente em exercícios práticos.", order: 1 }
    ]
  }
];

// Tira os acentos do texto para facilitar a busca
function normalizeTextClient(text) {
  if (!text) return '';
  return text.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().trim();
}

// Filtra a lista local caso a API esteja desligada
function filterFallbackCourses(filters = {}) {
  let list = [...FALLBACK_COURSES];
  if (filters.search && filters.search.trim()) {
    const term = normalizeTextClient(filters.search);
    list = list.filter(c =>
      normalizeTextClient(c.fullName).includes(term) ||
      normalizeTextClient(c.shortName).includes(term) ||
      normalizeTextClient(c.description).includes(term)
    );
  }
  if (filters.category && filters.category.trim()) {
    const cat = normalizeTextClient(filters.category);
    list = list.filter(c => normalizeTextClient(c.category) === cat);
  }
  if (filters.modality && filters.modality.trim()) {
    const mod = normalizeTextClient(filters.modality);
    list = list.filter(c => normalizeTextClient(c.modality) === mod);
  }
  if (filters.status && filters.status.trim()) {
    const st = normalizeTextClient(filters.status);
    list = list.filter(c => normalizeTextClient(c.status) === st);
  }
  return {
    total: FALLBACK_COURSES.length,
    filteredTotal: list.length,
    items: list,
    availableFilters: {
      categories: [...new Set(FALLBACK_COURSES.map(c => c.category))].sort(),
      modalities: [...new Set(FALLBACK_COURSES.map(c => c.modality))].sort(),
      statuses: [...new Set(FALLBACK_COURSES.map(c => c.status))].sort()
    }
  };
}

// Busca a lista de cursos na API aplicando os filtros
async function fetchCourses(filters = {}) {
  const params = new URLSearchParams();
  if (filters.search) params.set('search', filters.search);
  if (filters.category) params.set('category', filters.category);
  if (filters.modality) params.set('modality', filters.modality);
  if (filters.status) params.set('status', filters.status);

  const query = params.toString();
  const url = `${API_BASE_URL}/api/courses${query ? '?' + query : ''}`;

  let data;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Erro ${response.status}: Não foi possível carregar os cursos.`);
    }
    data = await response.json();
  } catch (err) {
    if (err instanceof TypeError) {
      data = filterFallbackCourses(filters);
    } else {
      throw err;
    }
  }

  let items = [];
  if (Array.isArray(data)) {
    items = data;
  } else if (data && Array.isArray(data.items)) {
    items = data.items;
  }

  items.items = items;
  items.total = (data && typeof data.total === 'number') ? data.total : items.length;
  items.filteredTotal = (data && typeof data.filteredTotal === 'number') ? data.filteredTotal : items.length;
  items.availableFilters = data?.availableFilters || {};

  return items;
}

// Busca os detalhes de um curso pelo ID
async function fetchCourseById(id) {
  const url = `${API_BASE_URL}/api/courses/${id}`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Curso não encontrado.');
      }
      throw new Error(`Erro ${response.status}: Falha ao buscar detalhes do curso.`);
    }
    return await response.json();
  } catch (err) {
    if (err instanceof TypeError) {
      const found = FALLBACK_COURSES.find(c => String(c.id) === String(id));
      if (!found) {
        throw new Error('Curso não encontrado.');
      }
      return found;
    }
    throw err;
  }
}

// Retorna o ícone e a cor da tag de acordo com a categoria
function getCategoryInfo(category) {
  const map = {
    'Matemática': {
      icon: 'assets/icons/icon-matematica-20px.svg',
      badgeClass: 'badge-matematica'
    },
    'Ciências da Natureza': {
      icon: 'assets/icons/icon-ciencias-natureza-20px.svg',
      badgeClass: 'badge-ciencias'
    },
    'Sustentabilidade': {
      icon: 'assets/icons/icon-sustentabilidade-20px.svg',
      badgeClass: 'badge-sustentabilidade'
    },
    'Linguagens': {
      icon: 'assets/icons/icon-linguagens-20px.svg',
      badgeClass: 'badge-linguagens'
    }
  };

  return map[category] || {
    icon: 'assets/icons/icon-matematica-20px.svg',
    badgeClass: 'badge-matematica'
  };
}

// Cria o HTML de cada card de curso
function createCourseCardHTML(course, options = {}) {
  const cat = getCategoryInfo(course.category);
  let imgPath = course.imageUrl;
  if (imgPath && imgPath.startsWith('/')) {
    imgPath = imgPath.substring(1);
  }

  let descText = course.description || '';
  if (options.isLanding && course.id === 1 && descText.length > 120) {
    descText = 'Aplicação de grandezas, proporções e escalas em processos de corte, furação e usinagem.';
  }

  const metaHtml = options.isLanding
    ? ''
    : `
        <div class="card-meta">
          <span>${course.workloadHours} horas</span>
          <span>${course.modality}</span>
          <span class="card-status-pill">${course.status}</span>
        </div>
      `;

  return `
    <article class="course-card ${options.isLanding ? 'course-card-landing' : ''}" onclick="window.location.href='detalhes.html?id=${course.id}'" role="button" tabindex="0" aria-label="Ver detalhes do curso: ${course.fullName}">
      <div class="card-img-wrapper">
        <img src="${imgPath}" alt="" class="card-img" loading="lazy" />
        <div class="card-overlay"></div>
        <span class="badge-category ${cat.badgeClass}">
          <img src="${cat.icon}" alt="" aria-hidden="true" />
          ${course.category}
        </span>
      </div>
      <div class="card-body">
        <div>
          <h3 class="card-title">${course.fullName}</h3>
          <p class="card-desc">${descText}</p>
        </div>
        ${metaHtml}
      </div>
    </article>
  `;
}
