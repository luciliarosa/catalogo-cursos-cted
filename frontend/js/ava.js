document.addEventListener('DOMContentLoaded', async () => {
  const urlParams = new URLSearchParams(window.location.search);
  const courseId = urlParams.get('id') || '1';

  const titleEl = document.getElementById('ava-course-title');
  const btnBackDetails = document.getElementById('btn-back-details');
  const searchInput = document.getElementById('ava-search');
  const modulesNav = document.getElementById('ava-modules-nav');

  if (btnBackDetails) {
    btnBackDetails.href = `detalhes.html?id=${courseId}`;
  }

  // Carrega o nome do curso selecionado
  try {
    const currentCourse = await fetchCourseById(courseId);
    if (currentCourse && currentCourse.fullName) {
      if (titleEl) titleEl.textContent = currentCourse.fullName;
      document.title = `${currentCourse.fullName} | AVA SESI`;
    }
  } catch (err) {
    // mantém o título padrão se der erro
  }

  // Troca a aula ativa quando clica no menu lateral
  if (modulesNav) {
    const lessonLinks = modulesNav.querySelectorAll('.ava-lesson-link');
    lessonLinks.forEach(link => {
      link.addEventListener('click', (e) => {
        e.preventDefault();
        lessonLinks.forEach(l => {
          l.classList.remove('active');
          l.removeAttribute('aria-current');
        });
        link.classList.add('active');
        link.setAttribute('aria-current', 'step');
      });
    });
  }

  // Filtra os módulos na barra lateral conforme digita na busca
  if (searchInput && modulesNav) {
    searchInput.addEventListener('input', () => {
      const term = searchInput.value.trim().toLowerCase();
      const sections = modulesNav.querySelectorAll('.ava-nav-section');
      sections.forEach(sec => {
        const text = sec.textContent.toLowerCase();
        sec.style.display = (!term || text.includes(term)) ? '' : 'none';
      });
    });
  }
});
