import 'package:finders/Pages/forgotPassword.dart';
import 'package:finders/Pages/registerClient.dart';
import 'package:finders/Screens/topRated.dart';
import 'package:flutter/material.dart';

// LoginPartner page class
class Loginclient extends StatelessWidget {
  const Loginclient({super.key});

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
                  fontSize: 24,
                  color: Colors.black,  
                ),
              ),
            ),
          ),

          const SizedBox(height: 30), // Spacing

          // Login Text
          const Text(
            'Login',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 20,
              color: Colors.black,
            ),
          ),

          const SizedBox(height: 20), // Spacing

          // Email and Password Input Fields
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 40.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Email Text Field
                const Text('Email:'),
                const SizedBox(height: 5),
                TextField(
                  decoration: InputDecoration(
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5),
                      borderSide: const BorderSide(
                        color: Colors.orange,
                        width: 1.5,
                      ),
                    ),
                    filled: true,
                    fillColor: Colors.orange[50],
                  ),
                ),

                const SizedBox(height: 20), // Spacing

                // Password Text Field
                const Text('Password:'),
                const SizedBox(height: 5),
                TextField(
                  obscureText: true,
                  decoration: InputDecoration(
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5),
                      borderSide: const BorderSide(
                        color: Colors.orange,
                        width: 1.5,
                      ),
                    ),
                    filled: true,
                    fillColor: Colors.orange[50],
                  ),
                ),
              ],
            ),
          ),

          const SizedBox(height: 20), // Spacing

          // Links for register profile and forgot password
          Center(
            child: Column(
              children: [
                ElevatedButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => const TopRated()),
                    );
                  },
                  style: ElevatedButton.styleFrom(
                    padding: const EdgeInsets.symmetric(horizontal: 100, vertical: 15),
                    backgroundColor: Colors.blueAccent,
                    textStyle: const TextStyle(
                      fontSize: 16,
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  child: const Text('Login'),
                ),
                // Register profile link
                GestureDetector(
                  onTap: () {
                    // Navigate to RegistrationPage when tapped
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => const RegisterClient()),
                    );
                  },
                  child: const Text(
                    'register profile',
                    style: TextStyle(
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                // Forgot password link
                GestureDetector(
                  onTap: () {
                    // Navigate to ForgotPasswordPage when tapped
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => const ForgotPasswordPage()),
                    );
                  },
                  child: const Text(
                    'forgot password',
                    style: TextStyle(
                      color: Colors.red,
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



