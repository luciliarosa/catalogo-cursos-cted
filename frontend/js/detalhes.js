document.addEventListener('DOMContentLoaded', async () => {
  const loadingState = document.getElementById('loading-state');
  const errorState = document.getElementById('error-state');
  const errorMessage = document.getElementById('error-message');
  const courseContent = document.getElementById('course-content');

  // Pega o id do curso na URL (ex: ?id=1)
  const urlParams = new URLSearchParams(window.location.search);
  const courseId = urlParams.get('id') || '1';

  try {
    const course = await fetchCourseById(courseId);
    renderCourseDetails(course);
  } catch (err) {
    showError(err.message || 'Falha ao carregar informações do curso.');
  }

  // Exibe a tela de erro se o curso não existir
  function showError(msg) {
    if (loadingState) loadingState.style.display = 'none';
    if (courseContent) courseContent.style.display = 'none';
    if (errorState) errorState.style.display = 'block';
    if (errorMessage) errorMessage.textContent = msg;
  }

  // Formata a data para o padrão brasileiro (dd/mm/aaaa)
  function formatDateBR(dateInput) {
    if (!dateInput) return '05/01/2026';
    const raw = String(dateInput).split('T')[0].split(' ')[0];
    const parts = raw.split('-');
    if (parts.length === 3) {
      return `${parts[2]}/${parts[1]}/${parts[0]}`;
    }
    try {
      const d = new Date(dateInput);
      if (!isNaN(d.getTime())) {
        return d.toLocaleDateString('pt-BR');
      }
    } catch (e) {
      // ignora erro de data
    }
    return String(dateInput);
  }

  // Preenche os dados do curso na página
  function renderCourseDetails(course) {
    if (loadingState) loadingState.style.display = 'none';
    if (errorState) errorState.style.display = 'none';
    if (courseContent) courseContent.style.display = 'block';

    document.title = `${course.fullName} | SESI + Indústria`;

    const cat = getCategoryInfo(course.category);
    const badgeEl = document.getElementById('detail-category-badge');
    if (badgeEl) {
      badgeEl.className = `badge-category ${cat.badgeClass} detail-badge`;
    }
    const catIconEl = document.getElementById('detail-category-icon');
    if (catIconEl) catIconEl.src = cat.icon;
    const catNameEl = document.getElementById('detail-category-name');
    if (catNameEl) catNameEl.textContent = course.category;

    const titleEl = document.getElementById('detail-title');
    if (titleEl) titleEl.textContent = course.fullName;

    const dateEl = document.getElementById('detail-available-date');
    if (dateEl) {
      dateEl.textContent = Number(course.id) === 1 ? '05/01/2026' : formatDateBR(course.availableDate);
    }

    const certEl = document.getElementById('detail-certification');
    if (certEl) {
      certEl.textContent = 'SESI';
    }

    const detailImg = document.getElementById('detail-image');
    if (detailImg) {
      let imgPath = course.imageUrl || 'assets/images/video-thumb.png';
      if (imgPath.startsWith('/')) imgPath = imgPath.substring(1);
      if (Number(course.id) === 1) {
        imgPath = 'assets/images/video-thumb.png';
      }
      detailImg.src = imgPath;
      detailImg.alt = `Vídeo de apresentação do curso ${course.fullName}`;
    }

    const avaUrl = `ava.html?id=${course.id}`;
    const btnHeroAva = document.getElementById('btn-hero-ava');
    const btnFixedAva = document.getElementById('btn-fixed-ava');
    const navBtnAva = document.getElementById('nav-btn-ava');
    if (btnHeroAva) btnHeroAva.href = avaUrl;
    if (btnFixedAva) btnFixedAva.href = avaUrl;
    if (navBtnAva) navBtnAva.href = avaUrl;

    const descEl = document.getElementById('detail-full-description');
    if (descEl) {
      const mainDesc = Number(course.id) === 1
        ? 'Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.'
        : (course.description || '');

      descEl.innerHTML = `
        <p>${mainDesc}</p>
        <div class="course-extra-details" style="margin-top: 20px; padding-top: 16px; border-top: 1px solid #EAEAEA; font-size: 14px; color: #555555; line-height: 1.6;">
          ${course.shortName ? `<p style="font-size: 14px; margin-bottom: 6px; color: #444444;"><strong>Nome breve:</strong> ${course.shortName}</p>` : ''}
          ${course.modality ? `<p style="font-size: 14px; margin-bottom: 6px; color: #444444;"><strong>Modalidade:</strong> ${course.modality}</p>` : ''}
          ${course.workloadHours ? `<p style="font-size: 14px; margin-bottom: 6px; color: #444444;"><strong>Carga horária:</strong> ${course.workloadHours}h</p>` : ''}
          ${course.status ? `<p style="font-size: 14px; margin-bottom: 6px; color: #444444;"><strong>Status:</strong> ${course.status}</p>` : ''}
          ${course.targetAudience ? `<p style="font-size: 14px; margin-bottom: 0; color: #444444;"><strong>Público-alvo:</strong> ${course.targetAudience}</p>` : ''}
        </div>
      `;
    }

    // Para os cursos de ID 2 a 8, preenche os módulos, descrições de aulas e dúvidas vindos do banco.
    // No curso 1 mantém exatamente os 10 módulos e 10 perguntas do protótipo do Figma.
    if (Number(course.id) !== 1) {
      const modulesContainer = document.getElementById('modules-container');
      if (modulesContainer && Array.isArray(course.modules) && course.modules.length > 0) {
        modulesContainer.innerHTML = course.modules.map((mod, idx) => {
          const lessonsHtml = Array.isArray(mod.lessons) && mod.lessons.length > 0
            ? `<ul>${mod.lessons.map(l => `<li><strong>${l.title}</strong>${l.description ? ` — ${l.description}` : ''}${l.durationMinutes ? ` (${l.durationMinutes} min)` : ''}</li>`).join('')}</ul>`
            : '';
          return `
            <div class="figma-module-block">
              <h4>${mod.title || `${idx + 1}. Módulo ${idx + 1}`}</h4>
              ${lessonsHtml}
            </div>
          `;
        }).join('');
      }

      const faqBody = document.querySelector('#acc-body-faq .figma-accordion-content');
      if (faqBody && Array.isArray(course.faqs) && course.faqs.length > 0) {
        faqBody.innerHTML = course.faqs.map((f, idx) => `
          <div class="figma-faq-block">
            <h4>${idx + 1}. ${f.question}</h4>
            <p>${f.answer}</p>
          </div>
        `).join('');
      }
    }

    const fixedTitle = document.getElementById('fixed-title');
    if (fixedTitle) fixedTitle.textContent = course.fullName;

    setupAccordions();
    setupTestimonialsCarousel();
    setupFixedEnrollBar();
  }

  // Mostra a barra fixa de matrícula quando rola a página para baixo
  function setupFixedEnrollBar() {
    const fixedBar = document.getElementById('fixed-enroll-bar');
    if (!fixedBar) return;

    const handleScroll = () => {
      if (window.scrollY > 220) {
        fixedBar.classList.add('visible');
      } else {
        fixedBar.classList.remove('visible');
      }
    };

    window.addEventListener('scroll', handleScroll, { passive: true });
    handleScroll();
  }

  // Abre e fecha os accordions (Módulos, Aulas e Dúvidas)
  function setupAccordions() {
    const accordions = document.querySelectorAll('.accordion-item');

    accordions.forEach(item => {
      const header = item.querySelector('.accordion-header');
      const body = item.querySelector('.accordion-body');
      const iconImg = item.querySelector('.acc-icon-img');

      if (!header || !body) return;

      header.addEventListener('click', () => {
        const isOpen = body.classList.contains('open');

        if (isOpen) {
          body.classList.remove('open');
          header.setAttribute('aria-expanded', 'false');
          if (iconImg) iconImg.src = 'assets/icons/add_2.svg';
        } else {
          body.classList.add('open');
          header.setAttribute('aria-expanded', 'true');
          if (iconImg) iconImg.src = 'assets/icons/remove.svg';
        }
      });
    });
  }

  // Controla a rolagem dos depoimentos
  function setupTestimonialsCarousel() {
    const carousel = document.getElementById('detail-testimonials-carousel');
    const btnPrev = document.getElementById('btn-detail-test-prev');
    const btnNext = document.getElementById('btn-detail-test-next');

    if (carousel && btnPrev && btnNext) {
      btnPrev.addEventListener('click', () => {
        carousel.scrollBy({ left: -310, behavior: 'smooth' });
      });
      btnNext.addEventListener('click', () => {
        carousel.scrollBy({ left: 310, behavior: 'smooth' });
      });
      carousel.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowLeft') {
          carousel.scrollBy({ left: -310, behavior: 'smooth' });
        } else if (e.key === 'ArrowRight') {
          carousel.scrollBy({ left: 310, behavior: 'smooth' });
        }
      });
    }
  }
});
