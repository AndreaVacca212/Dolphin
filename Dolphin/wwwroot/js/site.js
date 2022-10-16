var email = document.modulo.email.value;
var email_reg_exp = /^([a-zA-Z0-9_.-])+@(Dolphin)+com/;
// var email_reg_exp = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-]{2,})+.)+([a-zA-Z0-9]{2,})+$/;


function controlla_registrazione() {
    if (document.getElementById('nome').value.length < 2 || document.getElementById('nome') == "") {
        alert("Nome troppo breve");
        return false;
    }

    if (document.getElementById('cognome').value.length < 2 || document.getElementById('cognome') == "") {
        alert("Cognome troppo breve");
        return false;
    }


    if (document.getElementById('pass').value != document.getElementById('confermapass').value) {
        alert("Le password non councidono");
        return false;
    }

    if (!email_reg_exp.test(email) || (email == "") || (email == "undefined")) {
        alert("Inserire un indirizzo email corretto.");
        document.modulo.email.select();
        return false;
    }
    let errors = [];

    if (document.getElementById('pass').value.length < 8)
        errors.push("La password deve avere almeno 8 caratteri");

    if (document.getElementById('pass').search(/[a-z]/) < 0)
        errors.push("La password deve contenere almeno 1 lettera minuscola");

    if (document.getElementById('pass').search(/[A-Z]/) < 0)
        errors.push("La password deve contenere almeno 1 lettera minuscola");

    if (document.getElementById('pass').search(/[0-9]/) < 0)
        errors.push("La password deve contenere almeno 1 numero");

    if (errors.length > 0) {
        alert(errors.join("\n"));
    }

    return true;
}

