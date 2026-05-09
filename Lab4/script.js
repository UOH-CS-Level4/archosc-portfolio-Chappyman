const themeToggle = document.getElementById('theme-toggle');

const savedTheme = localStorage.getItem('theme');

if (savedTheme === 'dark') {
  document.body.classList.add('dark-mode');
}

if (document.body.classList.contains('dark-mode')) {
  themeToggle.textContent = 'Light';
} else {
  themeToggle.textContent = 'Dark';
}

themeToggle.addEventListener('click', () => {
  document.body.classList.toggle('dark-mode');

  if (document.body.classList.contains('dark-mode')) {
    localStorage.setItem('theme', 'dark');
    themeToggle.textContent = 'Light';
  } else {
    localStorage.setItem('theme', 'light');
    themeToggle.textContent = 'Dark';
  }
});