# Flashcards Application Documentation

## Overview

The Flashcards application is a robust tool designed to assist users in learning through the use of flashcards. 
The application enables users to create, manage, and utilize flashcards and stacks of flashcards for effective learning. 
Additionally, it offers features for tracking study sessions and generating comprehensive reports on user progress 
and session details.

## Features

- Developed using C#, .NET Core and MS SQL Server.
- **Flashcard Management**: Users can create, delete, and edit flashcards, each containing a term and a definition.
- **Stack Management**: Users can organize flashcards into stacks, allowing for grouped study sessions focused on 
specific topics.
- **Study Sessions**: Engage in interactive study sessions where flashcards are presented and users can test their knowledge.
- **Progress Tracking**: The application tracks the progress of each session, recording user performance 
and improvements over time.
- **Report Generation**: Generate detailed reports in PDF format, summarizing user progress and session outcomes.
- **User-Friendly Interface**: The application features a simple and intuitive interface, making it easy to navigate
- **Customization**: Users can customize the application settings, including database connections and display options.
- **Data Persistence**: The application stores flashcards, stacks, and session data in a database, ensuring data integrity
## Installation

### Prerequisites
- .NET Core 3.1 SDK or later
- MS SQL Server or other compatible database system.

### Steps
1. Clone the repository from the provided URL.
2. Navigate to the project directory and restore the dependencies:
   ```
   dotnet restore
   ```
3. Set up the database by updating the connection string in the configuration files.

4. Run the application:
   ```
   dotnet run
   ```

## Usage

Once the application is running:
- Navigate through the main menu to manage flashcards and stacks.
- Choose the 'Start a Study Session' option to begin a session with your selected stack.
- Follow the on-screen prompts to answer flashcards and review session summaries.

## Architecture

The application is structured into several key components, ensuring a clean separation of concerns and promoting modularity:

- **Models**: Data structures for flashcards, stacks, and study sessions.
- **Data Access**: Manages database operations using repository patterns.
- **Services**: Contains helper services for managing study sessions and flashcard interactions.
- **View**: Handles display logic.
- **Commands**: Represents executable actions triggered from user inputs.
- **Repositories**: Provides an abstraction layer over direct data operations.
- **Handlers**: Manages editable entries and menu actions.
- **Database**: Handles connections and migrations for the SQL database.
- **Configuration**: Manages loading settings such as database connections.

## Forking

- Feel free to fork the repository and make changes to the application.
- This README provides all the necessary information to get started with the Flashcards application, 
ensuring users can effectively utilize and contribute to the project.