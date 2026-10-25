document.addEventListener('DOMContentLoaded', async () => {
  const header = document.getElementById('main-header');
  const headerLogo = document.getElementById('header-logo');
  const carousel = document.getElementById('courses-carousel');
  const btnPrev = document.getElementById('btn-courses-prev');
  const btnNext = document.getElementById('btn-courses-next');

  // Muda o fundo do menu e a cor da logo ao rolar a página
  if (header) {
    const handleScroll = () => {
      if (window.scrollY > 20) {
        header.classList.add('scrolled');
        if (headerLogo) {
          headerLogo.src = 'assets/icons/logo-sesi-industria-primary.svg';
        }
      } else {
        header.classList.remove('scrolled');
        if (headerLogo) {
          headerLogo.src = 'assets/icons/logo-sesi-industria-white.svg';
        }
      }
    };
    window.addEventListener('scroll', handleScroll, { passive: true });
    handleScroll();
  }

  // Busca os cursos da API para montar o carrossel da home
  try {
    const data = await fetchCourses();
    const courses = Array.isArray(data) ? data : (data?.items || []);
    if (courses.length > 0) {
      carousel.innerHTML = courses.map(course => createCourseCardHTML(course, { isLanding: true })).join('');

      // Permite abrir o card apertando Enter ou Espaço
      carousel.querySelectorAll('.course-card').forEach(card => {
        card.addEventListener('keydown', (e) => {
          if (e.key === 'Enter' || e.key === ' ') {
            e.preventDefault();
            card.click();
          }
        });
      });
    } else {
      carousel.innerHTML = '<p class="state-desc" style="padding: 20px;">Nenhum curso disponível no momento.</p>';
    }
  } catch (error) {
    console.error('Erro ao buscar cursos:', error);
    carousel.innerHTML = `
      <div class="error-box" style="margin: 0 auto;">
        <p class="error-title">Erro ao carregar cursos</p>
        <p class="state-desc">Verifique se a API está rodando na porta 5298.</p>
      </div>
    `;
  }

  // Botões de seta do carrossel de cursos
  if (btnPrev && btnNext && carousel) {
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

  // Botões de seta do carrossel de depoimentos
  const testCarousel = document.getElementById('testimonials-carousel');
  const btnTestPrev = document.getElementById('btn-test-prev');
  const btnTestNext = document.getElementById('btn-test-next');

  if (btnTestPrev && btnTestNext && testCarousel) {
    btnTestPrev.addEventListener('click', () => {
      testCarousel.scrollBy({ left: -310, behavior: 'smooth' });
    });
    btnTestNext.addEventListener('click', () => {
      testCarousel.scrollBy({ left: 310, behavior: 'smooth' });
    });
    testCarousel.addEventListener('keydown', (e) => {
      if (e.key === 'ArrowLeft') {
        testCarousel.scrollBy({ left: -310, behavior: 'smooth' });
      } else if (e.key === 'ArrowRight') {
        testCarousel.scrollBy({ left: 310, behavior: 'smooth' });
      }
    });
  }
});
