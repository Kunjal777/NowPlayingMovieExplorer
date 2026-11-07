# 🎬 Now Playing – Movie Explorer Web Application

## Software Requirements Specification (SRS)

### 1. Project Overview

**Now Playing – Movie Explorer** is a web application that allows users to explore movies, discover popular titles, browse movies currently playing in theaters, and search for movies by title. Users can also view detailed movie information and maintain a personalized list of favorite movies.

The application retrieves movie data from The Movie Database (TMDB) API and uses the browser's Local Storage to save users' favorite movies.

### 2. Purpose

The purpose of this application is to provide users with a simple, responsive, and user-friendly platform for discovering and exploring movies.

The application enables users to:

- Browse currently popular movies.
- Explore movies currently playing in theaters.
- Search for movies by title.
- View detailed information about movies.
- Add movies to a personal favorites list.
- Remove movies from their favorites list.
- Access their saved favorites using browser Local Storage.

### 3. Technology Stack

The application is developed using the following technologies:

| Technology | Description |
|---|---|
| C# | Primary programming language |
| .NET 8 | Application development framework |
| Blazor WebAssembly | Client-side application framework |
| Bootstrap | Responsive user interface design |
| HTML5 | Web page structure |
| CSS3 | Styling and layout |
| JavaScript | Browser functionality and integrations |
| TMDB API | Movie information and data retrieval |
| Browser Local Storage | Persistent storage of favorite movies |

**External Resources:**

- [TMDB API Documentation](https://developer.themoviedb.org/docs/getting-started)
- [Browser Local Storage Documentation](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage)

### 4. Functional Requirements

Functional requirements describe the features and operations that the application must provide to its users.

#### 4.1 Browse Movies

The application must allow users to discover and explore movies using data retrieved from the TMDB API.

The following functionalities must be available:

- **Popular Movies:** Display a collection of currently popular movies.
- **Now Playing Movies:** Display movies currently playing in theaters.
- **Search Movies:** Allow users to search for movies using their titles.
- **Movie Navigation:** Allow users to select a movie and navigate to its detailed information page.

#### 4.2 Favorite Movies

The application must provide a personalized favorites feature that allows users to manage their preferred movies.

The following functionalities must be available:

- Add any movie to the favorites list.
- Remove movies from the favorites list.
- View the complete list of saved favorite movies.
- Store favorite movies in the browser's Local Storage.
- Maintain saved favorites even after refreshing or reopening the browser, provided the browser's stored data is retained.

#### 4.3 Movie Details

The application must display detailed information about a selected movie.

The following information should be displayed whenever available through the TMDB API:

- Movie title, tagline, and poster.
- Movie overview or description.
- Release date.
- Movie runtime.
- Applicable movie genres.
- Overall user rating.
- Official movie trailer.
- Cast information, including actors' names and the characters they portray.

#### 4.4 Security

The application must follow appropriate security practices when accessing the TMDB API.

- The TMDB access token must not be exposed to the user's browser.
- Sensitive credentials must not be stored in publicly accessible client-side files.
- API credentials must be handled securely through an appropriate server-side mechanism.

### 5. User Interface Requirements

The application must provide an attractive, consistent, and user-friendly interface.

#### 5.1 Design and Layout

- Use a Bootstrap-based responsive design.
- Maintain consistent branding, colors, typography, and styling throughout the application.
- Provide a navigation menu accessible from all application pages.
- Ensure that users can easily access all major features.

#### 5.2 Responsive Design

- The application must support desktop, tablet, and mobile screen sizes.
- Page layouts and components must adjust according to different screen dimensions.
- Navigation and interactive elements must remain accessible and easy to use.

#### 5.3 Rendering

- The application must use Blazor WebAssembly for client-side rendering.
- HTML content must be generated and updated through the Blazor WebAssembly application.

#### 5.4 Application States and Error Handling

The application must handle different page and application states gracefully.

- **Loading State:** Display appropriate loading indicators while retrieving movie information.
- **Error State:** Show meaningful error messages when API requests fail or application exceptions occur.
- **Not Found State:** Display an appropriate message when users access invalid or unavailable URLs.
- **Empty State:** Display a suitable message when no movies are found or the favorites list is empty.

### 6. Application Limitations

The application has certain limitations due to its client-side architecture and dependency on external services.

#### 6.1 Local Storage Limitations

Since the application uses browser Local Storage to store favorite movies and does not include a centralized database or user authentication system:

- Favorite movies are stored locally in the user's browser.
- Favorites are specific to the browser and device being used.
- Users cannot automatically access their desktop favorites from their smartphones or other devices.
- Clearing browser data may result in the loss of saved favorite movies.

#### 6.2 TMDB API Dependency

The application relies on the TMDB API to retrieve movie information.

- Movie browsing, searching, and detailed information depend on TMDB API availability.
- If the TMDB service is unavailable, movie-related features may not function correctly.
- Previously saved favorite movies may still be accessible if their required data is stored in Local Storage.
- API request failures must be handled gracefully.

#### 6.3 Internet Connectivity

An active internet connection is required to retrieve movie information and images from the TMDB API.

### 7. Expected Outcome

The expected outcome is a fully functional, responsive movie browsing web application that provides users with an easy way to discover movies, search for titles, view detailed movie information, and manage their personal favorites list.

The application aims to provide a smooth and convenient movie discovery experience through a clean user interface and integration with the TMDB API.

### 8. Conclusion

Now Playing – Movie Explorer demonstrates the use of C#, .NET 8, Blazor WebAssembly, and external API integration to develop a modern web application.

It combines movie discovery, detailed information retrieval, and personalized favorites management while providing a responsive interface and browser-based data persistence.

---

**Developer:** Kunjal Salvi

**Project Type:** Web Application

**Framework:** Blazor WebAssembly (.NET 8)
