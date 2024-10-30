import 'package:flutter/material.dart';
import 'package:finders/Pages/loginClient.dart';  // Import the login client page
import 'package:finders/Pages/loginPartner.dart';  // Import the login partner page

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          // Top blue container with the app name
          Container(
            height: MediaQuery.of(context).size.height * 0.2,
            color: Colors.blueAccent,
            child: const Center(
              child: Text(
                'FINDERS',
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 20,
                  color: Colors.black,
                ),
              ),
            ),
          ),

          // Spacing
          const SizedBox(height: 50),

          // Buttons for "LOGIN AS PARTNER" and "LOGIN AS CLIENT"
          Center(
            child: Column(
              children: [
                // Login as Partner button
                TextButton(
                  onPressed: () {
                    // Navigate to LoginPartner screen when the button is pressed
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => const LoginPartner()),
                    );
                  },
                  style: TextButton.styleFrom(
                    backgroundColor: Colors.grey[300],
                    padding: const EdgeInsets.symmetric(vertical: 15, horizontal: 50),
                  ),
                  child: const Text(
                    'LOGIN AS PARTNER',
                    style: TextStyle(
                      color: Colors.black,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),

                const SizedBox(height: 20), // Space between the buttons

                // Login as Client button
                TextButton(
                  onPressed: () {
                    // Navigate to LoginClient screen when the button is pressed
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => const Loginclient()),
                    );
                  },
                  style: TextButton.styleFrom(
                    backgroundColor: Colors.grey[300],
                    padding: const EdgeInsets.symmetric(vertical: 15, horizontal: 50),
                  ),
                  child: const Text(
                    'LOGIN AS CLIENT',
                    style: TextStyle(
                      color: Colors.black,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ],
            ),
          ),

          const Spacer(), // Push the footer to the bottom

          // Bottom blue container for version info
          Container(
            height: MediaQuery.of(context).size.height * 0.1,
            color: Colors.blueAccent,
            child: const Center(
              child: Text(
                'v1.0.0',
                style: TextStyle(
                  color: Colors.white70,
                  fontSize: 14,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
