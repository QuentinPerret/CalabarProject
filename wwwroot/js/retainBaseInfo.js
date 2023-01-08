document.querySelector("form").addEventListener("submit", (e) => {
    localStorage.setItem('userName',document.getElementById('userNameId').value)
    localStorage.setItem('userSurname',document.getElementById('userSurnameId').value)
    localStorage.setItem('userEmail',document.getElementById('userEmailId').value)
})
