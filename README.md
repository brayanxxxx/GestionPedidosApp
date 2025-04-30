# TechExpress - Sistema de Gestión de Pedidos

## Descripción

TechExpress es una aplicación de Windows Forms desarrollada en C# que permite gestionar pedidos de una empresa de entregas. La aplicación implementa un sistema completo para registrar, procesar y dar seguimiento a pedidos de diferentes tipos de productos (tecnología, accesorios y componentes), con opciones para especificar la urgencia, peso del producto y distancia de entrega.

## Características

- **Registro de pedidos** con detalles completos
- **Selección automática del método de entrega** basada en:
  - Tipo de producto
  - Peso del producto
  - Distancia de entrega
  - Urgencia del pedido
- **Múltiples métodos de entrega**:
  - Dron
  - Motocicleta
  - Camión
  - Bicicleta (opción ecológica)
- **Historial de pedidos** con filtrado por tipo de entrega
- **Cálculo automático de costos** según el método de entrega

## Arquitectura

La aplicación está construida siguiendo varios patrones de diseño:

- **Patrón Singleton**: Implementado en `RegistroPedidos` para mantener una lista única de pedidos
- **Patrón Factory**: Implementado en `EntregaFactory` para determinar el método de entrega apropiado
- **Patrón Strategy**: Usado para los diferentes métodos de entrega a través de la interfaz `IMetodoEntrega`

## Estructura del Proyecto

```
GestionPedidosApp/
│
├── Form1.cs                  # Formulario principal
├── FormHistorial.cs          # Formulario para ver el historial de pedidos
├── Modelos/
│   └── Pedido.cs             # Clase modelo para los pedidos
│
├── Entrega/
│   ├── IMetodoEntrega.cs     # Interfaz para los métodos de entrega
│   ├── EntregaDron.cs        # Implementación de entrega por dron
│   ├── EntregaMotocicleta.cs # Implementación de entrega por motocicleta
│   ├── EntregaCamion.cs      # Implementación de entrega por camión
│   └── EntregaBicicleta.cs   # Implementación de entrega ecológica por bicicleta
│
└── Util/
    ├── EntregaFactory.cs     # Factory para crear el método de entrega apropiado
    └── RegistroPedidos.cs    # Singleton para mantener la lista de pedidos
```

## Reglas de Negocio

### Selección del Método de Entrega

El sistema selecciona automáticamente el método de entrega según estas reglas:

1. **Dron**:
   - Para productos de tecnología con peso < 5kg y entrega urgente
   - Costo: 10 * km

2. **Motocicleta**:
   - Para productos de tecnología con peso < 5kg y entrega no urgente
   - Para accesorios con peso < 10kg y entrega urgente
   - Costo: 5 * km

3. **Camión**:
   - Para productos de tecnología con peso >= 5kg
   - Para accesorios con peso >= 10kg
   - Para todos los componentes
   - Costo: 15 * km

4. **Bicicleta** (opción ecológica):
   - Para accesorios con peso < 2kg y entrega no urgente
   - Costo: 3 * km

## Uso de la Aplicación

1. En el formulario principal, complete los detalles del pedido:
   - Tipo de producto
   - Peso
   - Distancia
   - Urgencia

2. Haga clic en "Agregar Pedido" para procesar el pedido.

3. Para ver el historial completo de pedidos, haga clic en "Ver Historial Completo".

4. En el formulario de historial, puede filtrar los pedidos por tipo de entrega.

## Requisitos del Sistema

- Windows 7 o superior
- .NET Framework 4.5 o superior