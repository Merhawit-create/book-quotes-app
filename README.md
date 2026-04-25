# book-quotes-app
A full-stack web application built with Angular and .NET Web API, featuring CRUD operations for books and quotes with JWT authentication and responsive design.


# Book Quotes App

## Overview

This project is a full-stack web application that allows users to manage books and personal quotes with secure authentication.

The application demonstrates modern web development practices using a .NET Web API backend and an Angular frontend.

## Technologies

* Backend: ASP.NET Core Web API (.NET 9)
* Frontend: Angular
* Database:  SQL Server
* Authentication: JWT (JSON Web Tokens)
* Styling: Bootstrap & Font Awesome

## Features

* User registration and login
* JWT-based authentication
* CRUD operations for books
* CRUD operations for quotes
* Favorite quotes (⭐ toggle)
* Responsive design
* Navigation between Books and Quotes pages
*  Light/Dark mode toggle (🌞 / 🌙)

## Project Structure

book-quotes-app/

* backend/ → ASP.NET Core Web API
* frontend/ → Angular application

## Development Plan

### Phase 1: Backend Setup

* Create ASP.NET Core Web API project
* Configure controllers
* Setup database and DbContext

### Phase 2: Authentication

* Implement user registration
* Implement login
* Generate JWT tokens
* Secure API endpoints

### Phase 3: Books Feature

* Create Book entity
* Implement CRUD endpoints

### Phase 4: Quotes Feature

* Create Quote entity
* Implement CRUD endpoints

### Phase 5: Frontend (Angular)

* Create Angular project
* Implement authentication UI
* Connect to backend API

### Phase 6: UI & Styling

* Add Bootstrap styling
* Add Font Awesome icons
* Make application responsive

### Phase 7: Deployment

* Deploy backend (Render)
* Deploy frontend (Netlify or Vercel)

## Status

Frontend and backend completed.
Application includes authentication, CRUD features, favorites, and dark mode.
## Live Demo

Frontend (Netlify):
https://merhawit-book-quotes-app.netlify.app

Backend API (Render):
https://book-quotes-app-wdl3.onrender.com
