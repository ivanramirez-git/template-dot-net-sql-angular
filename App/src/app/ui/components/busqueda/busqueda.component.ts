import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { Observable } from 'rxjs';
import { TblCliente } from '../../../domain/models/tbl-cliente/tbl-cliente';
import { TblClienteUseCases } from '../../../domain/usecase/tbl-cliente-use-cases';
import { TblFacturaUseCases } from '../../../domain/usecase/tbl-factura-use-cases';
import { TblFactura } from '../../../domain/models/tbl-factura/tbl-factura';

@Component({
  selector: 'app-busqueda',
  templateUrl: './busqueda.component.html',
  styleUrls: ['./busqueda.component.scss'],
  providers: [NgModel],
})
export class BusquedaComponent implements OnInit {
  constructor(
    private _tblClienteUseCase: TblClienteUseCases,
    private _tblFacturaUseCase: TblFacturaUseCases
  ) { }
  responseClientes$: Observable<TblCliente[]> = new Observable<TblCliente[]>();
  responseFacturas$: Observable<TblFactura[]> = new Observable<TblFactura[]>();
  datosCliente: TblCliente[] = [];
  datosFactura: TblFactura[] = [];
  clienteSeleccionado: number | null = null;
  tipoBusqueda = 'cliente';

  ngOnInit(): void {
    this.responseClientes$ = this._tblClienteUseCase.getAllTblClientes();
    this.responseClientes$.subscribe((data) => {
      this.datosCliente = data;
      // Inicializa el cliente seleccionado si es necesario (puede ser null o el ID de un cliente por defecto)
      this.clienteSeleccionado = null; // Por ejemplo, inicializado como null
    });
  }

  toggleBusqueda(opcion: string) {
    this.tipoBusqueda = opcion;
  }

  isLoadingBotonBusqueda = false;
  buscar() {
    // Validar que se haya seleccionado un cliente y la busqueda sea por cliente
    if (this.clienteSeleccionado && this.tipoBusqueda == 'cliente') {
      this.buscarPorCliente();
    }

    // Validar que el texto de busqueda no este vacio y la busqueda sea por texto
    if (this.tipoBusqueda == 'numero-factura') {
      this.buscarPorNumeroFactura();
    }
  }

  buscarPorCliente() {
    // Accede al cliente seleccionado y muestra su nombre en la consola
    if (this.clienteSeleccionado) {
      const cliente = this.datosCliente.find(
        (c) => c.id == this.clienteSeleccionado
      );
      if (cliente) {
        this.isLoadingBotonBusqueda = true;
        console.log(cliente.razonSocial);
        this.responseFacturas$ =
          this._tblFacturaUseCase.getTblFacturasByCliente(cliente.id);
        this.responseFacturas$.subscribe((data) => {
          this.datosFactura = data;
          this.isLoadingBotonBusqueda = false;
        });
      }
    }
  }

  buscarPorNumeroFactura() {
    // Accede al texto de busqueda y muestra su contenido en la consola
    const textoBusqueda = (<HTMLInputElement>(
      document.getElementById('numero-factura')
    )).value;

    // Normalizar para que sea un numero
    let numeroFactura = 0;
    try {
      numeroFactura = parseInt(textoBusqueda);
      if (isNaN(numeroFactura)) {
        return;
      }
    } catch (e) {
      return;
    }

    this.isLoadingBotonBusqueda = true;
    this.responseFacturas$ =
      this._tblFacturaUseCase.getTblFacturasByNumber(numeroFactura);
    this.responseFacturas$.subscribe((data) => {
      this.datosFactura = data;
      this.isLoadingBotonBusqueda = false;
      console.log(this.datosFactura);
    });
  }
}
