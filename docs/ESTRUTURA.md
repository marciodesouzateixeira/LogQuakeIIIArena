[Voltar](../README.md)

# Estrutura de pastas da Solution

Abaixo segue uma visão da estrutura de pastas criadas para a solution LogQuake:

![estrutura de pastas](https://user-images.githubusercontent.com/44147082/47693213-84f68480-dbd7-11e8-8938-ff4c3e5bc1e7.PNG)


## Projetos:
1. **LogQuake.API.Test**
    - Projeto responsável por efetuar os testes da camada de API.
2. **LogQuake.API**
    - Projeto responsável por expor os recursos disponíveis para Consultar os logs do jogo e efetuar Upload de um novo log.
3. **LogQuake.Domain.Test**
    - Projeto responsável por efetuar os testes da camada de Domínio.
4. **LogQuake.Domain**
    - Projeto responsável por armazenar o Domínio do projeto.
5. **LogQuake.Service.Test**
    - Projeto responsável por efetuar os testes da camada de Serviço, validando as Consultas e Leitura de arquios.
6. **LogQuake.Service**
    - Projeto responsável por conter as regras de negócio do projeto.
7. **LogQuake.Infra.Test**
    - Projeto responsável efetuar os testes da camada de acesso ao Banco de Dados.
8. **LogQuake.Infra**
    - Projeto responsável por efetuar a leitura e escrita no Banco de Dados.
9. **LogQuake.CrossCutting**
    - Projeto responsável por conter rotinas que sejam utilizadas por qualquer camada do projeto.