## TECH TEST PAYMENT API

## Considerações e Premissas
- O teste foi realizado em C#12 (.NET 8) e codificado em inglês, com a premissa de seguir o título proposto da aplicação.

## Arquitetura, padrões e abordagens
- Arquitetura em camadas seguindo approach Domain Driven Design.
- SOLID
- DRY
- Design Patterns GoF.
- Unity of work
- Restfull
- Unit Tests
- Integration tests
- Graceful termination
- Conteinerização
- Observabilidade

Entre outros

## Requisitos para rodar o projeto
- Docker
- 256mb de espaço em disco (Aplicação + Instância MSSQL).
- 80mb de espaço em RAM (Aplicação).

## Como rodar o projeto
- Comando docker-compose up
- Para visualizar a documentação da api acesse: seu localhost [http: porta 80, https: porta 443] / api-docs

## Evidência de funcionamento
- Vídeo, Link: https://www.youtube.com/watch?v=BMhg_8gBGB4

### Licença
MIT

Contato: jonasm.1511@gmail.com


------------------------

# Enunciado

## INSTRUÇÕES PARA O TESTE TÉCNICO

- Crie um fork deste projeto (https://gitlab.com/Pottencial/tech-test-payment-api/-/forks/new). É preciso estar logado na sua conta Gitlab;
- Adicione @EmpresaOcultada (Empresa Ocultada) como membro do seu fork. Você pode fazer isto em  https://gitlab.com/`your-user`/tech-test-payment-api/settings/members;
 - Quando você começar, faça um commit vazio com a mensagem "Iniciando o teste de tecnologia" e quando terminar, faça o commit com uma mensagem "Finalizado o teste de tecnologia";
 - Commit após cada ciclo de refatoração pelo menos;
 - Não use branches;
 - Você deve prover evidências suficientes de que sua solução está completa indicando, no mínimo, que ela funciona;

## O TESTE
- Construir uma API REST utilizando .Net Core, Java ou NodeJs (com Typescript);
- A API deve expor uma rota com documentação swagger (http://.../api-docs).
- A API deve possuir 3 operações:
  1) Registrar venda: Recebe os dados do vendedor + itens vendidos. Registra venda com status "Aguardando pagamento";
  2) Buscar venda: Busca pelo Id da venda;
  3) Atualizar venda: Permite que seja atualizado o status da venda.
     * OBS.: Possíveis status: `Pagamento aprovado` | `Enviado para transportadora` | `Entregue` | `Cancelada`.
- Uma venda contém informação sobre o vendedor que a efetivou, data, identificador do pedido e os itens que foram vendidos;
- O vendedor deve possuir id, cpf, nome, e-mail e telefone;
- A inclusão de uma venda deve possuir pelo menos 1 item;
- A atualização de status deve permitir somente as seguintes transições: 
  - De: `Aguardando pagamento` Para: `Pagamento Aprovado`
  - De: `Aguardando pagamento` Para: `Cancelada`
  - De: `Pagamento Aprovado` Para: `Enviado para Transportadora`
  - De: `Pagamento Aprovado` Para: `Cancelada`
  - De: `Enviado para Transportador`. Para: `Entregue`
- A API não precisa ter mecanismos de autenticação/autorização;
- A aplicação não precisa implementar os mecanismos de persistência em um banco de dados, eles podem ser persistidos "em memória".

## PONTOS QUE SERÃO AVALIADOS
- Arquitetura da aplicação - embora não existam muitos requisitos de negócio, iremos avaliar como o projeto foi estruturada, bem como camadas e suas responsabilidades;
- Programação orientada a objetos;
- Boas práticas e princípios como SOLID, DDD (opcional), DRY, KISS;
- Testes unitários;
- Uso correto do padrão REST;

