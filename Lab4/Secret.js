const konamiCode = [
  "ArrowUp",
  "ArrowUp",
  "ArrowDown",
  "ArrowDown",
  "ArrowLeft",
  "ArrowRight",
  "ArrowLeft",
  "ArrowRight",
  "b",
  "a"
];

let userInput = [];

document.addEventListener("keydown", (e) => {
  userInput.push(e.key);

  if (userInput.length > konamiCode.length) {
    userInput.shift();
  }

  if (JSON.stringify(userInput) === JSON.stringify(konamiCode)) {
    activateEasterEgg();
  }
});

function activateEasterEgg() {
  document.body.classList.add("rainbow-mode");

  const notification = document.createElement("div");

  notification.classList.add("secret-notification");

  notification.textContent = "Secret Theme Unlocked";

  document.body.appendChild(notification);
}