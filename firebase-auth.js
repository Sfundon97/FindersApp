// firebase-auth.js
document.addEventListener("DOMContentLoaded", function () {
    const auth = firebase.auth();

    // Check if the user is already authenticated
    auth.onAuthStateChanged((user) => {
        if (user) {
            // Redirect authenticated users to Home/Index
            window.location.href = "/Home/Index";
        }
    });

    // Handle login form submission
    document.getElementById("loginForm").addEventListener("submit", function (e) {
        e.preventDefault();
        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;

        auth.signInWithEmailAndPassword(email, password)
            .then((userCredential) => {
                // Signed in
                const user = userCredential.user;
                console.log("Logged in as:", user.email);
                // Redirect to Home/Index after successful login
                window.location.href = "/Home/Index";
            })
            .catch((error) => {
                console.error("Error signing in:", error);
                alert("Login failed: " + error.message);
            });
    });
});
