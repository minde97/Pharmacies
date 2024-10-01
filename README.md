### Application
Application contains two parts, one part is API which is built using .NET 8 and other part is a react application with typescript. 

### Running Application
- Clone respository
- Run API
  - Set configuration
  - Build Pharmacies solution
  - Run Pharmacies api
- Run frontend application 
  - In ./phrmacies-app run `npm i`
  - After success run `npm run dev`
- Open application `http://localhost:5000/`

### Dev notes - To Do
- Solution missing log table for record insert/updates tracking 

### Docker support
Solution has docker support for easier setup
- In root directory run `docker-compose up`
- Wait for containers to start
- Open application `http://localhost:5000/`