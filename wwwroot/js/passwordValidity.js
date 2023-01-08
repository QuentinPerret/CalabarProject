document.querySelector("form").addEventListener("submit", (e) => {

  // Récupération des informations saisies par l'utilisateur
  const password = document.getElementById("password").value;
  const passwordVerification = document.getElementById("passwordVerification").value;
  // console.log(`${password} , ${passwordVerification}`)
  
  // Vérification éventuelles
  if(password != passwordVerification){
    e.preventDefault(); // Annule l'envoi du formulaire au serveur web
    document.getElementById("passwordVerificationDangerText").style.display = 'block'
  }
  else{
    document.getElementById("passwordVerificationDangerText").style.display = 'none'
  }
});