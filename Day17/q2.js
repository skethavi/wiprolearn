document.getElementById('fetchUserBtn').addEventListener('click', () => {
  fetch('https://randomuser.me/api/')
    .then(response => response.json())
    .then(data => {
      const user = data.results[0];
      const name = `${user.name.title} ${user.name.first} ${user.name.last}`;
      const email = user.email;
      const picture = user.picture.large;

      document.getElementById('userInfo').innerHTML = `
        <h2>${name}</h2>
        <p>Email: ${email}</p>
        <img src="${picture}" alt="Profile Picture">
      `;
    })
    .catch(error => {
      document.getElementById('userInfo').innerHTML = 'Error fetching user data.';
      console.error("Error fetching user data:", error);
      alert("Error fetching user data. Please try again later.");
    });
});