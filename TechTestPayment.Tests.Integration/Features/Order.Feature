# language: pt
# encoding: utf-8

Funcionalidade: Gerenciamento de pedidos
    Como um sistema integrador
    Quero poder utilizar operações de CRUD de pedidos
    Para poder gerenciar os pedidos

Contexto: Criação de vendedor e produto
  Dado que um vendedor é criado com nome "Pottencialito Seguradorus"
  E um produto é criado com nome "Vale presente de seguro garantia"

@clean_sellers
@clean_products
Cenario: Criação de pedido
    Dado que adiciono um produto com o nome "Vale presente de seguro garantia" com quantidade "5"
    E que informo o vendedor com o nome "Pottencialito Seguradorus"
    E submeto o post para a rota orders
    Então o pedido é criado com sucesso
    E o pedido deve constar no banco de dados

@clean_sellers
@clean_products
Cenario: Criação de pedido com vendendor inválido
    Dado que adiciono um produto com o nome "Vale presente de seguro garantia" com quantidade "5"
    E que informo o vendedor com o nome "Concorrentus Seguradorus"
    Então ao submeter deve lançar um erro com a mensagem "[POT-006] The seller does not exist."

@clean_sellers
@clean_products
Cenario: Criação de pedido com produto inválido
    Dado que adiciono um produto com o nome "Seguro automotivo" com quantidade "5"
    E que informo o vendedor com o nome "Pottencialito Seguradorus"
    Então ao submeter deve lançar um erro com a mensagem "[POT-009] The product does not exist."

@clean_sellers
@clean_products
Cenario: Transição completa de status de pedido com sucesso
    Dado que crio um pedido novo
    E atualizo o status do pedido para "PaymentApproved"
    E atualizo o status do pedido para "Sent"
    E atualizo o status do pedido para "Delivered"
    Então o status do pedido deve ser "Delivered"

@clean_sellers
@clean_products
Cenario: Novo pedido deve conter status "WaitingPayment"
    Dado que crio um pedido novo
    Então o status do pedido deve ser "WaitingPayment"
    E o pedido deve constar na base com o status "WaitingPayment"

@clean_sellers
@clean_products
Cenario: Cancelamento de pedido com status "WaitingPayment"
    Dado que crio um pedido novo
    E atualizo o status do pedido para "Cancelled"
    Então o status do pedido deve ser "Cancelled"
    E o pedido deve constar na base com o status "Cancelled"

@clean_sellers
@clean_products
Cenario: Cancelamento de pedido com status "PaymentApproved"
    Dado que crio um pedido novo
    E atualizo o status do pedido para "PaymentApproved"
    E atualizo o status do pedido para "Cancelled"
    Então o status do pedido deve ser "Cancelled"
    E o pedido deve constar na base com o status "Cancelled"

@clean_sellers
@clean_products
Cenario: Cancelamento de pedido com status "Sent"
    Dado que crio um pedido novo
    E atualizo o status do pedido para "PaymentApproved"
    E atualizo o status do pedido para "Sent"
    E informo o status do pedido para "Cancelled"
    Quando submeto a atualização do status do pedido deve lançar erro "[POT-004] Not allowed to change from status [Sent] to requested status [Cancelled]."
    E o pedido deve constar na base com o status "Sent"

@clean_sellers
@clean_products
Cenario: Cancelamento de pedido com status "Delivered"
    Dado que crio um pedido novo
    E atualizo o status do pedido para "PaymentApproved"
    E atualizo o status do pedido para "Sent"
    E atualizo o status do pedido para "Delivered"
    E informo o status do pedido para "Cancelled"
    Quando submeto a atualização do status do pedido deve lançar erro "[POT-004] Not allowed to change from status [Delivered] to requested status [Cancelled]."
    E o pedido deve constar na base com o status "Delivered"

@clean_sellers
@clean_products
Cenario: Cancelamento de pedido com status "Cancelled"
    Dado que crio um pedido novo
    E atualizo o status do pedido para "Cancelled"
    E informo o status do pedido para "Cancelled"
    Quando submeto a atualização do status do pedido deve lançar erro "[POT-004] Not allowed to change from status [Cancelled] to requested status [Cancelled]."
    E o pedido deve constar na base com o status "Cancelled"