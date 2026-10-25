document.addEventListener('DOMContentLoaded', () => {
  const inputSearch = document.getElementById('input-search');
  const btnSearch = document.getElementById('btn-search');
  const selectCategory = document.getElementById('select-category');
  const selectModality = document.getElementById('select-modality');
  const selectStatus = document.getElementById('select-status');
  const btnClearFilters = document.getElementById('btn-clear-filters');
  const btnEmptyClear = document.getElementById('btn-empty-clear');
  const btnRetry = document.getElementById('btn-retry');

  const loadingState = document.getElementById('loading-state');
  const errorState = document.getElementById('error-state');
  const errorMessage = document.getElementById('error-message');
  const emptyState = document.getElementById('empty-state');
  const coursesGrid = document.getElementById('courses-grid');
  const resultsCount = document.getElementById('results-count');

  // Pega os filtros da URL caso venha de outra página
  const urlParams = new URLSearchParams(window.location.search);
  if (urlParams.has('search')) inputSearch.value = urlParams.get('search');
  if (urlParams.has('category')) selectCategory.value = urlParams.get('category');
  if (urlParams.has('modality')) selectModality.value = urlParams.get('modality');
  if (urlParams.has('status')) selectStatus.value = urlParams.get('status');

  // Busca os cursos na API passando os filtros selecionados
  async function loadCourses() {
    showLoading();

    const filters = {
      search: inputSearch.value.trim(),
      category: selectCategory.value,
      modality: selectModality.value,
      status: selectStatus.value
    };

    try {
      const data = await fetchCourses(filters);
      const courses = Array.isArray(data) ? data : (data.items || []);
      const count = (data && typeof data.filteredTotal === 'number') ? data.filteredTotal : courses.length;
      renderCourses(courses, count);
    } catch (err) {
      showError(err.message || 'Erro ao comunicar com a API.');
    }
  }

  // Monta os cards de curso na tela
  function renderCourses(courses, count) {
    loadingState.style.display = 'none';
    errorState.style.display = 'none';

    const list = Array.isArray(courses) ? courses : [];
    const total = typeof count === 'number' ? count : list.length;

    if (list.length === 0) {
      coursesGrid.innerHTML = '';
      emptyState.style.display = 'block';
      resultsCount.textContent = '0 cursos encontrados';
      return;
    }

    emptyState.style.display = 'none';
    resultsCount.textContent = total === 1 ? '1 curso encontrado' : `${total} cursos encontrados`;

    coursesGrid.innerHTML = list.map(course => createCourseCardHTML(course)).join('');

    const cards = coursesGrid.querySelectorAll('.course-card');
    cards.forEach(card => {
      card.addEventListener('keydown', (e) => {
        if (e.key === 'Enter' || e.key === ' ') {
          e.preventDefault();
          card.click();
        }
      });
    });
  }

  // Mostra o spinner de carregamento
  function showLoading() {
    loadingState.style.display = 'block';
    errorState.style.display = 'none';
    emptyState.style.display = 'none';
    coursesGrid.innerHTML = '';
    resultsCount.textContent = 'Buscando cursos...';
  }

  // Mostra mensagem caso dê erro na API
  function showError(msg) {
    loadingState.style.display = 'none';
    emptyState.style.display = 'none';
    coursesGrid.innerHTML = '';
    errorState.style.display = 'block';
    errorMessage.textContent = msg;
    resultsCount.textContent = 'Erro na consulta';
  }

  // Limpa os campos de busca e recarrega a lista
  function clearAllFilters() {
    inputSearch.value = '';
    selectCategory.value = '';
    selectModality.value = '';
    selectStatus.value = '';
    loadCourses();
  }

  btnSearch.addEventListener('click', loadCourses);

  inputSearch.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      loadCourses();
    }
  });

  // Espera o usuário terminar de digitar para buscar
  let debounceTimeout = null;
  inputSearch.addEventListener('input', () => {
    clearTimeout(debounceTimeout);
    debounceTimeout = setTimeout(() => {
      loadCourses();
    }, 400);
  });

  selectCategory.addEventListener('change', loadCourses);
  selectModality.addEventListener('change', loadCourses);
  selectStatus.addEventListener('change', loadCourses);

  btnClearFilters.addEventListener('click', clearAllFilters);
  btnEmptyClear.addEventListener('click', clearAllFilters);
  btnRetry.addEventListener('click', loadCourses);

  loadCourses();
});
