import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CatProducto } from '../../../domain/models/cat-producto/cat-producto';
import { TblCliente } from '../../../domain/models/tbl-cliente/tbl-cliente';
import { TblDetalleFactura } from '../../../domain/models/tbl-detalle-factura/tbl-detalle-factura';
import { TblFactura } from '../../../domain/models/tbl-factura/tbl-factura';
import { CatProductoUseCases } from '../../../domain/usecase/cat-producto-use-cases';
import { TblClienteUseCases } from '../../../domain/usecase/tbl-cliente-use-cases';
import { TblDetalleFacturaUseCases } from '../../../domain/usecase/tbl-detalle-factura-use-cases';
import { TblFacturaUseCases } from '../../../domain/usecase/tbl-factura-use-cases';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.scss'],
})
export class FacturasComponent implements OnInit {
  constructor(
    private _tblClienteUseCase: TblClienteUseCases,
    private _catProductoUseCases: CatProductoUseCases,
    private _tblFacturaUseCase: TblFacturaUseCases,
    private _tlbDetalleFacturaUseCase: TblDetalleFacturaUseCases
  ) { }
  responseClientes$: Observable<TblCliente[]> = new Observable<TblCliente[]>();
  responseProductos$: Observable<CatProducto[]> = new Observable<CatProducto[]>();

  datosCliente: TblCliente[] = [];
  datosProductos: CatProducto[] = [];
  datosFactura: TblFactura = {
    id: 0,
    fechaEmisionFactura: new Date(),
    idCliente: 0,
    numeroDeFactura: 0,
    numeroDeProductos: 0,
    subTotalFactura: 0,
    totalImpuestos: 0,
    totalFactura: 0,
    createdAt: new Date(),
    updatedAt: new Date(),
    deletedAt: null,
    cliente: null,
    detalleFactura: [],
  };

  ngOnInit() {
    this.nuevoFactura();
    this.responseClientes$ = this._tblClienteUseCase.getAllTblClientes();
    this.responseClientes$.subscribe((data) => {
      this.datosCliente = data;
    });
    this.responseProductos$ = this._catProductoUseCases.getAllCatProductos();
    this.responseProductos$.subscribe((data) => {
      this.datosProductos = data;
    });
  }

  clienteSeleccionadoChange() {
    if (this.datosFactura.idCliente) {
      const cS = this.datosCliente.find(
        (c) => c.id == this.datosFactura.idCliente
      );
      if (cS) {
        // Asigna la información del cliente a datosFactura
        this.datosFactura.idCliente = cS.id;
        console.log(cS);
        this.datosFactura.cliente = cS;
        // Puedes asignar más propiedades del cliente si es necesario
      }
    }
  }

  isLoadingNumeroFactura = false;
  numeroFacturaChange() {
    this.isLoadingNumeroFactura = true;
    let responseFacturas$: Observable<TblFactura[]> =
      this._tblFacturaUseCase.getTblFacturasByNumber(
        this.datosFactura.numeroDeFactura!
      );
    responseFacturas$.subscribe((data) => {
      if (data.length > 0) {
        // Si existe, muestra un mensaje de error
        alert('El numero de factura ya existe');
        this.datosFactura.numeroDeFactura = null;
      }
      this.isLoadingNumeroFactura = false;
    });
  }

  nuevoFactura() {

    const botonGuardar = <HTMLInputElement>(
      document.getElementById('guardar-button-crear-factura')
    );
    botonGuardar.disabled = false;
    this.datosFactura = {
      id: 0, // Asigna el valor correspondiente
      fechaEmisionFactura: new Date(), // Asigna la fecha actual o la deseada
      idCliente: null, // Asigna el valor correspondiente
      numeroDeFactura: null, // Asigna el valor correspondiente
      numeroDeProductos: 0, // Puedes inicializarlo en 0
      subTotalFactura: 0, // Puedes inicializarlo en 0
      totalImpuestos: 0, // Puedes inicializarlo en 0
      totalFactura: 0, // Puedes inicializarlo en 0
      createdAt: new Date(), // Asigna la fecha actual o la deseada
      updatedAt: new Date(), // Asigna la fecha actual o la deseada
      deletedAt: null, // Puedes asignar null
      cliente: null, // Debes asignar un objeto TblCliente
      detalleFactura: [], // Debes asignar un arreglo de TblDetalleFactura si es necesario
    };
  }

  agregarProducto() {
    // TODO: Implementar la logica para agregar un producto a la factura
    // Buscar el producto 1 en datosProducto y agregarlo a datosFactura.detalleFactura
    const producto = this.datosProductos.find((p) => p.id == 1);
    if (producto) {
      this.datosFactura.detalleFactura.push(<TblDetalleFactura>{
        id: 0,
        idFactura: this.datosFactura.id,
        idProducto: producto.id,
        cantidadDeProducto: 1,
        precioUnitario: producto.precio,
        createdAt: new Date(),
        notas: '',
        updatedAt: new Date(),
        deletedAt: null,
        subTotal: producto.precio,
        producto: producto,
      });
    }
    // Calcular los totales
    this.calcularTotales();
  }

  guardarFactura() {
    // Validar que el cliente seleccionado
    if (!this.datosFactura.cliente) {
      alert('El cliente no ha sido seleccionado');
      return;
    }
    // Validar que el numero de factura no exista
    if (!this.datosFactura.numeroDeFactura) {
      alert('El numero de factura no ha sido ingresado');
      return;
    }
    // Validar que exista al menos un producto seleccionado
    if (this.datosFactura.detalleFactura.length == 0) {
      alert('No hay productos seleccionados');
      return;
    }
    let responseFactura$: Observable<TblFactura> =
      this._tblFacturaUseCase.createTblFactura(this.datosFactura);
    responseFactura$.subscribe((data) => {
      // Validar que si se creo
      if (data.id) {
        // Crear cada detalle de factura
        this.datosFactura.detalleFactura.forEach((d) => {
          d.idFactura = data.id;
          let responseDetalleFactura$: Observable<TblDetalleFactura> =
            this._tlbDetalleFacturaUseCase.createTblDetalleFactura(d);
          responseDetalleFactura$.subscribe((data) => {
            console.log(data);
          });
        });
        // Consultar la factura creada
        let responseFactura$: Observable<TblFactura> =
          this._tblFacturaUseCase.getTblFacturaById(data.id);
        responseFactura$.subscribe((data) => {
          if (data) {
            const botonGuardar = <HTMLInputElement>(
              document.getElementById('guardar-button-crear-factura')
            );
            botonGuardar.disabled = true;
            alert('Factura creada exitosamente');
          } else {
            alert('Error al crear la factura');
          }
        });
      } else {
        alert('Error al crear la factura');
      }
    });
  }

  calcularTotales() {
    // Validar que los productos seleccionados son los que estan en datosFactura.detalleFactura

    if (this.datosFactura.detalleFactura.length > 0) {
      this.datosFactura.numeroDeProductos =
        this.datosFactura.detalleFactura.reduce(
          (a, b) => a + b.cantidadDeProducto,
          0
        );
      this.datosFactura.subTotalFactura =
        this.datosFactura.detalleFactura.reduce(
          (a, b) => a + b.precioUnitario * b.cantidadDeProducto,
          0
        );
      this.datosFactura.totalImpuestos =
        this.datosFactura.detalleFactura.reduce(
          (a, b) => a + b.precioUnitario * b.cantidadDeProducto * 0.19,
          0
        );
      this.datosFactura.totalFactura =
        this.datosFactura.subTotalFactura + this.datosFactura.totalImpuestos;
      // subtotales de los detalles
      this.datosFactura.detalleFactura.forEach((d) => {
        d.subTotal = d.precioUnitario * d.cantidadDeProducto;
      });
    }
  }

  cantidadProductoChange(detalle: TblDetalleFactura, sumar: boolean) {
    const indiceProducto = this.datosFactura.detalleFactura.indexOf(detalle);
    if (sumar) {
      detalle.cantidadDeProducto++;
    } else {
      if (detalle.cantidadDeProducto === 1) {
        // Si la cantidad es 1 y se resta, elimina el producto
        this.datosFactura.detalleFactura.splice(indiceProducto, 1);
      } else {
        detalle.cantidadDeProducto--;
      }
    }
    //  actualizar otros valores en datosFactura
    this.calcularTotales();
  }

  productoSeleccionadoChange(detalle: TblDetalleFactura) {
    // Verifica si se seleccionó un producto válido (no nulo)
    if (detalle.producto) {
      // Agrega el producto a la lista de productos seleccionados
      const indiceProducto = this.datosFactura.detalleFactura.indexOf(detalle);
      this.datosFactura.detalleFactura[indiceProducto].producto =
        detalle.producto;
      this.datosFactura.detalleFactura[indiceProducto].precioUnitario =
        detalle.producto.precio;
      this.datosFactura.detalleFactura[indiceProducto].subTotal =
        detalle.producto.precio * detalle.cantidadDeProducto;
      this.datosFactura.detalleFactura[indiceProducto].idProducto =
        detalle.producto.id;
      //  actualizar otros valores en datosFactura
      this.calcularTotales();
    }
  }
}
