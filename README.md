# Gerenciamento de produtos e notas
Esse projeto foi desenvolvido como parte do processo seletivo para vaga de desenvolvedor fullstack na Korp do grupo Viasoft do teste técnico para vaga na empresa [Korp](https://www.korp.com.br/) do grupo Viasoft

## Descrição do Projeto
Esse projeto solicita o desenvolvimento de aplicações que façam gerenciamento de produtos em estoque e de notas fiscais relacionandos os produtos, com a comunicação entre o site e os serviços, o projeto deve ser capaz de apresentar uma visualização dos produtos e notas, como também adicioná-las.


Segue a descrição do projeto enviada pelo time da Korp:
```
Escopo:
Desenvolvimento de aplicação com intuito de impressão de nota fiscal, para isso deve ser possível:
 
1 - Cadastrar produtos no sistema (Além de informações básicas de cadastro o produto deve ter um controle de saldo);
3 - Cadastrar notas fiscais (numeração e status (aberto, fechada)) no sistema, em uma nota poderá ser inserido vários produtos previamente cadastrados no sistema;
4 - Uma nota fiscal aberta com produtos pode ser impressa;
 
Comportamentos esperados:
 
1 - Ao imprimir a nota o sistema deve validar saldo de estoque do produto;
2 - Caso seja possível imprimir a nota, o saldo do produto deve ser baixado e status da nota alterada;
3 - Usuário deve receber feedback ao fim do processamento;
 
Desafios obrigatórios execução:
É esperado uma estrutura de micro serviços, tendo no mínimo dois serviços, um para controle do estoque e outro para o faturamento;
É esperado que as etapas do processo contemplem o conceito de ACID;
É necessário um exemplo em que um dos micro serviços irá falhar, e o sistema possa se recuperar e usuário tenha feedback necessário;
 
Desafios extras:
Tratamento de concorrência de requisições;
```

# Detalhes técnicos
## Backend
Os serviços para gerenciamento de produtos e notas foram feitos em ASP.NET, utilizando a arquitetura de [Minimal API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api). O projeto utiliza EntityFramework para gerenciar comunicação entre os serviços e a base SQL Server.

## Frontend
O frontend foi desenvolvido utlizando [Angular](https://angular.dev/), na versão 19.2.3. Foram utilizados conceitos de environments, arquitetura de estilos com scss e rotas.

# Features

## Pendências
- Impressão de nota fiscal
- Retorno de status de erro ao cliente

## Melhorias
- Responsividade
- Adicionar _tooltips_ para informar ações na tela
- Adicionar tratamento para retentativas para erro de comunicação
- Observalidade nos serviços - Logs de input
