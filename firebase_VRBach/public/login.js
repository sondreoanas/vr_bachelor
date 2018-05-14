var config = {
    apiKey: "AIzaSyCUJZDBvuEI0Snh2oJZ_l6mO5C4JRFOkn8",
    authDomain: "htcvruis2018.firebaseapp.com",
    databaseURL: "https://htcvruis2018.firebaseio.com",
    projectId: "htcvruis2018",
    storageBucket: "htcvruis2018.appspot.com",
    messagingSenderId: "311579352975"
};
firebase.initializeApp(config);

const btnLogin = document.getElementById("login");
const txtUsername = document.getElementById("username");
const txtPassword = document.getElementById("password");

btnLogin.addEventListener('click', e => {
    const username = txtUsername.value;
    const password = txtPassword.value;
    const auth = firebase.auth();
    const promise = auth.signInWithEmailAndPassword(username,password);
    promise.catch(e => console.log(e.message));
});

firebase.auth().onAuthStateChanged(user => {
    if (user) {
        location.href = "./admin.html";
    } else {
        console.log("logged out!")
    }
});