import 'package:finders/Screens/services.dart';
import 'package:flutter/material.dart';

class TopRated extends StatelessWidget {
  const TopRated({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue[200],
        title: const Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            CircleAvatar(
              backgroundImage: NetworkImage('https://images-platform.99static.com//yHXhWx8e6BhBhnNcQAFbEnZnaiI=/341x74:915x648/fit-in/500x500/99designs-contests-attachments/116/116036/attachment_116036922'), // Placeholder image
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
          Container(
            color: Colors.pink[50],
            padding: const EdgeInsets.symmetric(horizontal: 10),
            child: const TextField(
              decoration: InputDecoration(
                prefixIcon: Icon(Icons.search),
                hintText: 'Search Category or Keyword',
                border: InputBorder.none,
              ),
            ),
          ),
          // Categories section
          SizedBox(
            height: 50,
            child: ListView(
              scrollDirection: Axis.horizontal,
              children: [
                _buildCategoryChip('Plumbing'),
                _buildCategoryChip('Beauty'),
                _buildCategoryChip('Painting'),
                _buildCategoryChip('Glass'),
                _buildCategoryChip('Electronics'),
              ],
            ),
          ),
          Expanded(
            child: ListView(
              children: [
                _buildServiceCard(
                  context,
                  'Nomonde Beauty',
                  'Hair and Nail Technician',
                  '750ZAR - 3500ZAR',
                  'Bloemfontein, West Dene',
                  '051 673 2134/084 999 2486',
                  4.0,
                ),
                _buildServiceCard(
                   context,
                  'Kani Inc.',
                  'Plumbing at your service',
                  '500ZAR - 2500ZAR',
                  'Botshabelo, J section',
                  '051 673 2134/084 999 2486',
                  4.5,
                ),
                _buildServiceCard(
                   context,
                  'Nomonde Beauty',
                  'Hair and Nail Technician',
                  '750ZAR - 3500ZAR',
                  'Bloemfontein, West Dene',
                  '051 673 2134/084 999 2486',
                  4.0,
                ),
                _buildServiceCard(
                   context,
                  'Pablo Paints',
                  'Colour Your Life',
                  '670ZAR - 5500ZAR',
                  'Bloemfontein, Willows',
                  '051 673 2134/084 999 2486',
                  5.0,
                ),
                // Add more cards as needed
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

  Widget _buildCategoryChip(String label) {
    return Padding(
      padding: const EdgeInsets.all(4.0),
      child: Chip(
        label: Text(label),
        backgroundColor: Colors.blue[100],
      ),
    );
  }

  Widget _buildServiceCard(BuildContext context, String title, String subtitle,
      String priceRange, String location, String contact, double rating) {
    return GestureDetector(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) => ServiceProviderServices(
              serviceName: title,
              location: location,
              services: const [
                {'name': 'Manicure', 'price': 250, 'selected': false},
                {'name': 'Weave install', 'price': 650, 'selected': false},
                {'name': 'Make Up', 'price': 300, 'selected': false},
                {'name': 'Pedicure', 'price': 300, 'selected': false},
                {'name': 'Massage', 'price': 650, 'selected': false},
                {'name': 'Facial', 'price': 650, 'selected': false},
              ],
            ),
          ),
        );
      },
      child: Card(
        margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
        child: Padding(
          padding: const EdgeInsets.all(10),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const CircleAvatar(
                backgroundImage: NetworkImage('https://www.askattest.com/wp-content/uploads/2022/08/iStock-1331637318.jpg'), // Placeholder image
              ),
              const SizedBox(width: 10),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(title, style: const TextStyle(fontWeight: FontWeight.bold)),
                    Text(subtitle),
                    Text(priceRange),
                    Text(location),
                    Text(contact),
                    Row(
                      children: List.generate(
                        5,
                        (index) => Icon(
                          index < rating ? Icons.star : Icons.star_border,
                          color: Colors.amber,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
