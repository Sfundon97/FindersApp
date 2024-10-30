import 'dart:io';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:finders/Screens/partner_profile_page.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

class RegisterPartner extends StatefulWidget {
  const RegisterPartner({super.key});

  @override
  _RegisterPartnerState createState() => _RegisterPartnerState();
}

class _RegisterPartnerState extends State<RegisterPartner> {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController categoryController = TextEditingController();
  final TextEditingController companyNameController = TextEditingController();
  final TextEditingController dateJoinedController = TextEditingController();
  final TextEditingController registrationNumberController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  final TextEditingController confirmPasswordController = TextEditingController();

  File? _profileImage;
  final ImagePicker _picker = ImagePicker();

  // Pick image from gallery
  Future<void> _pickImage() async {
    final pickedImage = await _picker.pickImage(source: ImageSource.gallery);
    if (pickedImage != null) {
      setState(() {
        _profileImage = File(pickedImage.path);
      });
    }
  }

  // Register partner and save data to Firestore and Firebase Storage
  Future<void> registerPartner() async {
    String email = emailController.text;
    String category = categoryController.text;
    String companyName = companyNameController.text;
    String dateJoined = dateJoinedController.text;
    String registrationNumber = registrationNumberController.text;
    String password = passwordController.text;
    String confirmPassword = confirmPasswordController.text;

    // Validate passwords
    if (password != confirmPassword) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Passwords do not match')),
      );
      return;
    }

    // Validate profile image
    if (_profileImage == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please select a profile picture')),
      );
      return;
    }

    try {
      // Create Firebase user using Firebase Authentication
      UserCredential userCredential = await FirebaseAuth.instance
          .createUserWithEmailAndPassword(email: email, password: password);

      String uid = userCredential.user!.uid;

      // Upload profile image to Firebase Storage
      final ref = FirebaseStorage.instance
          .ref()
          .child('profile_pictures')
          .child('$uid.jpg');
      await ref.putFile(_profileImage!);
      final profileImageUrl = await ref.getDownloadURL();

      // Save partner details to Firestore
      await FirebaseFirestore.instance.collection('partners').doc(uid).set({
        'email': email,
        'category': category,
        'companyName': companyName,
        'dateJoined': dateJoined,
        'registrationNumber': registrationNumber,
        'profileImageUrl': profileImageUrl,
      });

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Partner registered successfully')),
      );

      // Clear inputs and image
      emailController.clear();
      categoryController.clear();
      companyNameController.clear();
      dateJoinedController.clear();
      registrationNumberController.clear();
      passwordController.clear();
      confirmPasswordController.clear();
      setState(() {
        _profileImage = null;
      });

      // Navigate to profile page
      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) => PartnerProfilePage(
            email: email,
            profileImageUrl: profileImageUrl,
            companyName: companyName,
            category: category,
            dateJoined: dateJoined,
            registrationNumber: registrationNumber,
          ),
        ),
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error: $e')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.pop(context);
          },
        ),
        title: const Text('FINDERS'),
        backgroundColor: Colors.blueAccent,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // Top section with app name
              Container(
                height: MediaQuery.of(context).size.height * 0.1,
                color: Colors.blueAccent,
                child: const Center(
                  child: Text(
                    'Register',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 20,
                      color: Colors.black,
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 15),

              // Profile Image
              GestureDetector(
                onTap: _pickImage,
                child: CircleAvatar(
                  radius: 50,
                  backgroundImage:
                      _profileImage != null ? FileImage(_profileImage!) : null,
                  child: _profileImage == null
                      ? const Icon(Icons.camera_alt, size: 50)
                      : null,
                ),
              ),
              const SizedBox(height: 20),

              // Input fields
              TextField(
                controller: emailController,
                decoration: const InputDecoration(
                  labelText: 'Email',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: categoryController,
                decoration: const InputDecoration(
                  labelText: 'Category',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: companyNameController,
                decoration: const InputDecoration(
                  labelText: 'Company Name',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: dateJoinedController,
                decoration: const InputDecoration(
                  labelText: 'Date Joined',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: registrationNumberController,
                decoration: const InputDecoration(
                  labelText: 'Registration Number',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: passwordController,
                obscureText: true,
                decoration: const InputDecoration(
                  labelText: 'Password',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 20),

              TextField(
                controller: confirmPasswordController,
                obscureText: true,
                decoration: const InputDecoration(
                  labelText: 'Confirm Password',
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 30),

              // Register button
              Center(
                child: ElevatedButton(
                  onPressed: registerPartner,
                  style: ElevatedButton.styleFrom(
                    padding: const EdgeInsets.symmetric(
                        horizontal: 100, vertical: 15),
                    backgroundColor: Colors.blueAccent,
                    textStyle: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  child: const Text('Register'),
                ),
              ),
              const SizedBox(height: 20),

              // Bottom section with version text
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
        ),
      ),
    );
  }
}
