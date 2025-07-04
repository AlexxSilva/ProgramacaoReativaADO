# 📦 PedidoMonitorApp

[![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![Rx.NET](https://img.shields.io/badge/Rx.NET-Reactive-blueviolet?logo=reactivex)](https://github.com/dotnet/reactive)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> Projeto de monitoramento reativo de pedidos utilizando **Rx.NET** no ecossistema **.NET**, com foco em **Clean Code**, **DDD** e boas práticas arquiteturais.

---

## 💡 Descrição

Este projeto aplica o **paradigma reativo** com `Observable` (Rx.NET) para monitorar periodicamente uma base de dados e detectar **novos pedidos** com status `"NP"`. Quando um pedido é identificado, ele é automaticamente **exibido e atualizado** para o status `"P"`.

A estrutura segue os princípios de **Clean Code**, **Domain-Driven Design (DDD)**, com foco na separação de responsabilidades, reutilização de código e testabilidade.

---

## 🚀 Tecnologias Utilizadas

- [NET FRAMEWORK 4.7](https://dotnet.microsoft.com/)
- [Rx.NET (System.Reactive)](https://github.com/dotnet/reactive)
- SQL Server
- ADO.NET (para acesso direto ao banco)

---

## 📐 Padrões e Princípios Aplicados

- ✅ **Paradigma Reativo** com `Observable`
- ✅ **Padrão Repository**
- ✅ **Padrão Service**
- ✅ **Padrão Observer**
- ✅ **DDD (Domain-Driven Design)**
- ✅ **SRP (Single Responsibility Principle)**
- ✅ **DIP (Dependency Inversion Principle)**
- ✅ **Injeção de Dependência**

---

## 🗂️ Estrutura do Projeto

PedidoMonitorApp/
├── Domain/
│ ├── Pedido.cs
│ └── IPedidoRepository.cs
│ └── IPedidoService.cs
│
├── Application/
│ ├── PedidoService.cs
│ └── PedidoMonitor.cs
│
├── Infrastructure/
│ └── PedidoRepository.cs
│
├── UI/
│ ├── IPedidoExibidor.cs
│ └── PedidoExibidorConsole.cs
│
└── Program.cs


