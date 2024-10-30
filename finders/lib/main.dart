import 'package:finders/Pages/login1.dart'; // Ensure this path is correct
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';

void main() async {
  // Ensure the Flutter bindings are initialized
  WidgetsFlutterBinding.ensureInitialized();

  // Initialize Firebase
  try {
    await Firebase.initializeApp();
  } catch (e) {
    print("Error initializing Firebase: $e"); // Handle initialization error
  }

  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false, // Hide the debug banner
      title: 'Finders App',
      theme: ThemeData(
        primarySwatch: Colors.blue, // Set a theme if desired
      ),
      home: const LoginPage(), // Starting point of the app
    );
  }
}
