# Lexlume

**Learn. Practice. Remember.**

Lexlume is a language-learning application designed to help users build and retain language knowledge through vocabulary practice and interactive exercises.

The project starts with German as its first learning language, but it is being designed with a language-independent foundation so that additional languages can be supported in the future.

> One platform. Every language. Endless possibilities.

---

## Table of Contents

- [Project Vision](#project-vision)
- [Core Principles](#core-principles)
- [Technology Stack](#technology-stack)
- [Application Roles](#application-roles)
- [Roadmap](#roadmap)
- [Current Release: v0.1.0](#current-release-v010)
- [Future Plans](#future-plans)
- [Project Status](#project-status)

---

## Project Vision

The goal of Lexlume is to provide a simple, flexible and enjoyable environment for learning foreign languages.

Rather than relying on fixed learning material, the application will allow learning content to be organized and managed within the platform. Users will be able to practice vocabulary and, as the application evolves, apply what they have learned through sentences, grammar exercises, reading and writing activities.

The initial focus is to establish a reliable foundation for vocabulary management and practice. Additional learning features will be introduced gradually through planned releases.

Although German is the first language planned for the application, the underlying design should support multiple languages without requiring fundamental changes to the application.

## Core Principles

- **Language independent:** Learning content should not be tied to one specific language.
- **Incremental development:** Features will be introduced through small, clearly defined releases.
- **Active learning:** Users should recall and apply knowledge rather than only read translations.
- **Repetition:** Previously learned material should be available for continued practice.
- **Extensibility:** The application should be easy to expand with new exercise types and learning features.

---

## Technology Stack

The planned technology stack is:

| Area | Technology |
|---|---|
| Frontend | Angular |
| Backend | C# / ASP.NET Core |
| Database | Microsoft SQL Server |

The application will use a frontend/backend architecture, with the backend responsible for business logic, data access, authentication and authorization.

---

## Application Roles

Lexlume will initially support two roles.

### Administrator

The administrator is responsible for managing the learning content.

Initial permissions:

- Access the administration area.
- Create vocabulary entries.
- View and search vocabulary.
- Edit existing vocabulary.
- Delete vocabulary entries.

Administrator access will be assigned directly through the database. Public registration must not allow users to select or assign themselves the administrator role.

### Student

The student is the standard role assigned to newly registered users.

Initial permissions:

- Register and log in.
- Access vocabulary practice.
- Select a learning level.
- Complete vocabulary exercises.
- Review the results of a completed exercise batch.

Authorization must be enforced by the backend, not only by hiding frontend pages or navigation items.

---

## Roadmap

The project will follow incremental versioning. Each release will have a defined scope and its own set of tasks and acceptance criteria.

| Version | Planned scope | Status |
|---|---|---|
| v0.1.0 | Authentication, roles, vocabulary management and basic vocabulary practice | Planned |
| v0.2.0 | Additional vocabulary exercise types | Planned |
| v0.3.0 | Learning progress and practice history | Planned |
| ... |
| v1.0.0 | First complete stable release | Future |

The roadmap is an initial direction, not a fixed commitment. Scope and priorities may change as the application develops.

---

## Current Release: v0.1.0

**Release goal:** Establish the application's foundation and deliver a working vocabulary learning experience.

This release intentionally focuses on a small set of features. Advanced exercises, learning statistics and additional learning modules are outside its scope.

### v0.1.0 Definition of Done

The release can be considered complete when:

- [ ] Users can register, log in and log out.
- [ ] Newly registered users receive the Student role.
- [ ] Administrator access is managed securely.
- [ ] Administrators can create, view, edit and delete vocabulary.
- [ ] Singular and plural forms can be managed as separate entries.
- [ ] Students can select a CEFR level.
- [ ] Students can complete a 30-question vocabulary batch when enough entries exist.
- [ ] Each question provides four answer options and immediate feedback.
- [ ] Students can review their results after completing the batch.
- [ ] Students cannot access administrator functionality.

---

## Future Plans

The following are possible directions for future development. They are not commitments for a specific release.

### Vocabulary Exercises

- English-to-German typing exercises.
- German-to-English translation.
- Article selection.
- Singular-to-plural exercises.
- Word matching.

### Sentence Practice

- Fill in missing words.
- Arrange words into correct sentences.
- Translate complete sentences.
- Write sentences using selected vocabulary.

### Grammar

- Grammar topic management.
- Grammar explanations and examples.
- Exercises organized by CEFR level.

### Reading and Writing

- German reading passages.
- Reading comprehension questions.
- Text transcription.
- Free writing exercises.

### Learning Progress

- Spaced repetition.
- Vocabulary mastery tracking.
- Practice history.
- Weak-word review.
- Learning statistics and achievements.

### Multi-language Support

The application is intended to support multiple learning languages in the future. Learning content and exercises should therefore be associated with language information rather than being hardcoded for German.

---

## Project Status

Lexlume is currently in its initial planning and development stage.

The first milestone is **v0.1.0**, focused on authentication, vocabulary management and basic multiple-choice vocabulary practice.

Features and release plans documented here may be updated as development progresses.
