import 'package:flutter/material.dart';

class ServiceProviderServices extends StatelessWidget {
  final String serviceName;
  final String location;
  final List<Map<String, dynamic>> services;

  const ServiceProviderServices({super.key, 
    required this.serviceName,
    required this.location,
    required this.services,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue[200],
        title: const Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            CircleAvatar(
              backgroundImage: NetworkImage(
                  'https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'),
            ),
            Text(
              'FINDERS',
              style: TextStyle(color: Colors.black),
            ),
            Icon(Icons.menu, color: Colors.black),
          ],
        ),
      ),
      body: Column(
        children: [
          // Location
          Padding(
            padding: const EdgeInsets.all(10.0),
            child: Text(
              '$serviceName\n$location',
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 18,
              ),
              textAlign: TextAlign.center,
            ),
          ),
          // Service images
          Container(
            height: 150,
            padding: const EdgeInsets.symmetric(horizontal: 10),
            child: GridView.count(
              crossAxisCount: 2,
              children: [
                Image.network('https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'),
                Image.network('https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'),
                Image.network('https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'),
                Image.network('https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'),
              ],
            ),
          ),
          const SizedBox(height: 10),
          // Services List
          Expanded(
            child: ListView.builder(
              itemCount: services.length,
              itemBuilder: (context, index) {
                final service = services[index];
                return ListTile(
                  title: Text(service['name']),
                  trailing: Text('R${service['price']}'),
                  leading: Checkbox(
                    value: service['selected'],
                    onChanged: (value) {
                      // Handle the service selection change here
                    },
                  ),
                );
              },
            ),
          ),
          // Total and Book Button
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  'TOTAL: Rxxxx',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
                ),
                ElevatedButton(
                  onPressed: () {
                    // Handle booking action
                  },
                  child: const Text('Book Now'),
                ),
              ],
            ),
          ),
        ],
      ),
      bottomNavigationBar: BottomNavigationBar(
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: '',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.language),
            label: '',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.phone),
            label: '',
          ),
        ],
      ),
    );
  }
}
