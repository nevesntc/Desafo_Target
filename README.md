# Desafio Técnico - Target Sistemas

[![CI](https://github.com/nevesntc/Desafo_Target/actions/workflows/ci.yml/badge.svg)](https://github.com/nevesntc/Desafo_Target/actions/workflows/ci.yml)

Desenvolvido por **Bruno Neves** ([@nevesntc](https://github.com/nevesntc)) como parte do processo seletivo para
Desenvolvedor de Sistemas Jr. na Target Sistemas.

Minha solução em C# (.NET 8) para os três exercícios do desafio:

1. Cálculo de comissão dos vendedores
2. Movimentação de estoque
3. Cálculo de juros por atraso

Optei por reunir os três em um único programa de console, com um menu, e organizar o código em camadas para manter
as regras de negócio separadas da entrada/saída de dados e fáceis de testar.

## Como executar

Pré-requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
dotnet run --project src/DesafioTarget.App
```

Para rodar os testes:

```bash
dotnet test
```

A cada push na `main` o GitHub Actions compila a solução e roda todos os testes automaticamente
(workflow em `.github/workflows/ci.yml`).

## Estrutura

```
src/
  DesafioTarget.Domain          regras de negócio (entidades, serviços e interfaces)
  DesafioTarget.Infrastructure  leitura dos arquivos JSON e repositórios
  DesafioTarget.App             aplicação console (menu e interação com o usuário)
tests/
  DesafioTarget.Tests           testes unitários (xUnit)
```

O `Domain` não depende de nenhum outro projeto. A `Infrastructure` implementa as interfaces de repositório definidas
no domínio e o `App` é quem monta as dependências (em `Program.cs`) e conversa com o usuário.

Os arquivos de dados fornecidos no desafio estão em `src/DesafioTarget.App/Dados`.

## Decisões

### 1. Comissão

- A regra de comissão fica atrás da interface `IPoliticaComissao` (padrão **Strategy**). A implementação
  `PoliticaComissaoPorFaixa` recebe as faixas de valor, então mudar os percentuais ou criar outra regra
  não exige alterar a `CalculadoraComissao`.
- Faixas usadas: abaixo de R$ 100,00 → 0%; de R$ 100,00 até R$ 499,99 → 1%; a partir de R$ 500,00 → 5%.
- A comissão é calculada **venda a venda** e arredondada em 2 casas, depois somada por vendedor.
- Valores monetários usam `decimal` para evitar erro de arredondamento de ponto flutuante.

Resultado com os dados do desafio:

| Vendedor        | Vendas | Total vendido | Comissão  |
|-----------------|-------:|--------------:|----------:|
| João Silva      | 10     | R$ 10.754,70  | R$ 495,69 |
| Maria Souza     | 9      | R$ 9.874,30   | R$ 465,96 |
| Ana Lima        | 9      | R$ 8.763,95   | R$ 404,99 |
| Carlos Oliveira | 8      | R$ 7.928,35   | R$ 379,38 |

### 2. Estoque

- `Produto` controla o próprio saldo (`DarEntrada` / `DarSaida`) e não deixa o estoque ficar negativo.
- Cada movimentação recebe um número sequencial único, tipo (entrada ou saída), quantidade, descrição e data/hora,
  e retorna o **estoque final** do produto.
- Acesso a dados através do padrão **Repository** (`IProdutoRepository`, `IMovimentacaoRepository`).
  O estoque inicial é carregado do JSON e as movimentações ficam em memória durante a execução. Para persistir
  em banco (ex.: SQL Server) bastaria criar novas implementações dos repositórios, sem mexer no `EstoqueService`.

### 3. Juros

- Juros simples de 2,5% por dia de atraso: `juros = valor × 2,5% × dias em atraso`.
- Se o vencimento for hoje ou uma data futura, não há juros.
- A data de hoje vem de um `TimeProvider` injetado, o que permite testar o cálculo com uma data fixa.

### Menu

Cada opção do menu implementa `IOpcaoMenu` (padrão **Command**). Para incluir uma nova funcionalidade basta criar
uma nova classe e registrá-la no `Program.cs`, sem alterar o `MenuPrincipal`.

## Contato

Bruno Neves - [github.com/nevesntc](https://github.com/nevesntc)
