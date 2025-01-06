BlazorApp1 - Dockerized Blazor Project
Table of Contents
1. Prerequisites
2. Getting Started
3. Build the Docker Image
4. Run the Application Locally
5. Push Docker Image to Docker Hub
6. Access the Application
7. Project Structure
8. Troubleshooting

1. Prerequisites
Before you begin, ensure that you have the following installed:
- Docker Desktop (Windows/macOS/Linux) – Download Docker Desktop (https://www.docker.com/products/docker-desktop)
- .NET SDK 8.0 – For building and running the Blazor application (Download .NET SDK https://dotnet.microsoft.com/download/dotnet)
- A Docker Hub account – To push the Docker image to a Docker registry (Sign Up on Docker Hub https://hub.docker.com/signup)
2. Getting Started
1. Clone the Repository:
If you haven’t cloned the repository yet, use the following command to clone the project:
git clone https://github.com/Ami9a-Bibi/Docker.git
cd Docker

2. Open the Project:
Open the BlazorApp1 project in your preferred code editor (e.g., Visual Studio Code, Visual Studio).
3. Build the Docker Image
1. Build the Docker Image:
In the project folder where the Dockerfile is located, run the following command to build the Docker image:
docker build -t blazorapp1:latest .

2. Tag the Docker Image:
After building the image, tag it for Docker Hub:
docker tag blazorapp1:latest quratainy1/blazorapp1:latest
Replace "quratainy1" with your Docker Hub username.
4. Run the Application Locally
1. Run the Docker Image Locally:
To run the application in a Docker container, use the following command:
docker run -d -p 5151:8080 --name blazorapp1-container quratainy1/blazorapp1:latest

2. Access the Application:
Open your browser and navigate to:
http://localhost:5151
Your Blazor application should now be accessible.
5. Push Docker Image to Docker Hub
To share your Docker image on Docker Hub, follow these steps:

1. Login to Docker Hub:
Log in to Docker Hub using your credentials:
docker login

2. Push the Docker Image to Docker Hub:
Push your image to Docker Hub with the following command:
docker push quratainy1/blazorapp1:latest
This will upload the image to your Docker Hub repository.
6. Access the Application
After pushing the Docker image to Docker Hub, you (or anyone with the image name) can pull and run it:

1. Pull the Image:
To pull the image from Docker Hub:
docker pull quratainy1/blazorapp1:latest

2. Run the Image:
To run the pulled image locally:
docker run -d -p 5151:8080 --name blazorapp1-container quratainy1/blazorapp1:latest
Then, open the browser and navigate to http://localhost:5151 to see the application.
7. Project Structure
- Dockerfile: Contains instructions for building and running the Docker container.
- BlazorApp1: The main Blazor WebAssembly project.
  - wwwroot: Contains static files like CSS, images, etc.
  - Pages: Blazor pages such as the homepage, etc.
  - Program.cs: Entry point for the Blazor application.

2. Port Conflicts:
- Ensure that no other application is using port 5151 on your machine.
- You can change the port mapping in the docker run command if necessary.

3. Container Not Starting:
- Check the logs of the container using docker logs blazorapp1-container to find any errors.
- Make sure the container is running correctly by checking with docker ps.

- ![image](https://github.com/user-attachments/assets/81686c93-256a-4c08-b424-377033ea993a)

-
- ![image](https://github.com/user-attachments/assets/e17b69a3-cc2f-41f1-8e88-390ecce01b02)


- ![image](https://github.com/user-attachments/assets/1b36540d-4cb0-4e88-8299-2f08ad3a80b2)
![image](https://github.com/user-attachments/assets/c07df791-199d-4a52-a088-74e09f04f5e7)



![image](https://github.com/user-attachments/assets/2578991d-82a6-4636-8035-5609fcf0e936)
