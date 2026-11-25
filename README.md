<h1>Tradelog</h1>

<p>
  <img src="https://img.shields.io/badge/Status-work_in_progress-232526?style=for-the-badge&labelColor=DFDFDF&color=232526" alt="Status: work in progress">
</p>
<p>
  <img src="https://img.shields.io/badge/.NET_10-232526?style=for-the-badge&logo=dotnet&logoColor=DFDFDF" alt=".NET 10">
  <img src="https://img.shields.io/badge/C%23-232526?style=for-the-badge" alt="C#">
  <img src="https://img.shields.io/badge/EF_Core-232526?style=for-the-badge" alt="EF Core">
  <img src="https://img.shields.io/badge/PostgreSQL-232526?style=for-the-badge&logo=postgresql&logoColor=DFDFDF" alt="PostgreSQL">
  <img src="https://img.shields.io/badge/Docker-232526?style=for-the-badge&logo=docker&logoColor=DFDFDF" alt="Docker">
</p>
<p>
  <img src="https://img.shields.io/badge/React_19-232526?style=for-the-badge&logo=react&logoColor=DFDFDF" alt="React">
  <img src="https://img.shields.io/badge/TypeScript-232526?style=for-the-badge&logo=typescript&logoColor=DFDFDF" alt="TypeScript">
  <img src="https://img.shields.io/badge/Tailwind_CSS-232526?style=for-the-badge&logo=tailwindcss&logoColor=DFDFDF" alt="Tailwind CSS">
  <img src="https://img.shields.io/badge/shadcn%2Fui-232526?style=for-the-badge&logo=shadcnui&logoColor=DFDFDF" alt="shadcn/ui">
</p>

> [!NOTE]
> 🚧 Tradelog is in an early stage. The foundations are in place and the first features are being built. This README will grow along with the project.

A personal **crypto trading journal**. It pulls your trades from an exchange API (starting with **OKX**, using a read-only key), shows them in a table and on a chart with entry and exit points, and lets you add your own notes, mood, tags and a rating to each trade.

**Why I'm building it:** I trade crypto and wanted a journal that fits the way I work. At the same time it's my main project for learning **backend development in .NET**. The goal isn't to ship as fast as possible, but to understand and write every piece of the backend myself.

## Planned features

- [ ] Importing trades from the OKX API (read-only key)
- [ ] Trade list with filters
- [ ] Chart with entry and exit points
- [ ] Notes, mood, tags and a rating for each trade
- [ ] CSV export
- [ ] Monthly report and figures for the annual tax return (PIT)
- [ ] Native Swift client (iOS / macOS) using the same API

## Current status

**Done**
- ASP.NET Core Web API (.NET 10) with controllers and OpenAPI
- PostgreSQL running in Docker, connected through EF Core, with the first migration applied
- React frontend set up with Vite, Tailwind and shadcn/ui
- Design tokens (light and dark theme) from the [design spec](./docs/design-spec.md)

**Next up**
- CORS for the frontend
- The first end-to-end feature: a `Trade` entity → `GET /api/trades` → trades table in the UI, starting with seeded test data. OKX integration will follow as a separate feature.

## Tech stack

| Layer | Technologies |
| --- | --- |
| **Backend** | .NET 10 · ASP.NET Core Web API (controllers) · EF Core 10 + Npgsql · OpenAPI |
| **Database** | PostgreSQL 18 (Docker) |
| **Frontend** | React 19 · TypeScript · Vite · Tailwind CSS 4 · shadcn/ui (Base UI) · Lucide · Zod |
| **Planned** | OKX API · Swift (iOS / macOS) |

## Project structure

```
Tradelog/
├── backend/
│   ├── backend.slnx
│   └── Tradelog.Api/       # ASP.NET Core Web API
│       ├── Controllers/
│       ├── Data/           # AppDbContext
│       └── Migrations/     # EF Core migrations
├── frontend/               # Vite + React + shadcn/ui
├── docs/
│   └── design-spec.md      # design system and UI spec
└── docker-compose.yml      # local PostgreSQL
```

## Running locally

**Requirements:** .NET 10 SDK, Node.js with pnpm, Docker, and `dotnet-ef` 10.x (`dotnet tool install -g dotnet-ef --version 10.*`).

**1. Database** (from the repo root)

```bash
docker compose up -d
```

**2. Backend** (from `backend/Tradelog.Api`)

Secrets such as the connection string (and later the OKX keys) are kept in **user-secrets**, never in the repo:

```bash
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=tradelog;Username=tradelog;Password=<password from docker-compose.yml>"
dotnet ef database update
dotnet run                 # → http://localhost:5110
```

The OpenAPI document is available at `http://localhost:5110/openapi/v1.json`.

**3. Frontend** (from `frontend/`)

```bash
pnpm install
pnpm dev                   # → http://localhost:5173
```

---

<sub>More of my projects → <a href="https://maxtkaczyk.pl"><b>maxtkaczyk.pl</b></a></sub>
