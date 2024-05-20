import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CatProductoGateway } from './domain/models/cat-producto/gateway/cat-producto-gateway';
import { CatProductoApiService } from './infraestructure/driven-adapter/cat-producto/cat-producto-api.service';
import { HttpClientModule } from '@angular/common/http';
import { ConfigService } from './infraestructure/driven-adapter/config.service';
import { CatTipoClienteGateway } from './domain/models/cat-tipo-cliente/gateway/cat-tipo-cliente-gateway';
import { CatTipoClienteApiService } from './infraestructure/driven-adapter/cat-tipo-cliente/cat-tipo-cliente-api.service';
import { TblClienteGateway } from './domain/models/tbl-cliente/gateway/tbl-cliente-gateway';
import { TblClienteApiService } from './infraestructure/driven-adapter/tbl-cliente/tbl-cliente-api.service';
import { TblDetalleFacturaGateway } from './domain/models/tbl-detalle-factura/gateway/tbl-detalle-factura-gateway';
import { TblDetalleFacturaApiService } from './infraestructure/driven-adapter/tbl-detalle-factura/tbl-detalle-factura-api.service';
import { TblFacturaGateway } from './domain/models/tbl-factura/gateway/tbl-factura-gateway';
import { TblFacturaApiService } from './infraestructure/driven-adapter/tbl-factura/tbl-factura-api.service';
import { FacturasComponent } from './ui/components/facturas/facturas.component';
import { BusquedaComponent } from './ui/components/busqueda/busqueda.component';
import { InterruptorComponent } from './ui/common/interruptor/interruptor.component';
import { LoaderComponent } from './ui/common/loader/loader.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        AppComponent,
        FacturasComponent,
        BusquedaComponent,
        InterruptorComponent,
        LoaderComponent,
    ],
    imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
    providers: [
        // CatProducto
        { provide: CatProductoGateway, useClass: CatProductoApiService },
        // CatTipoCliente
        { provide: CatTipoClienteGateway, useClass: CatTipoClienteApiService },
        // TblCliente
        { provide: TblClienteGateway, useClass: TblClienteApiService },
        // TblDetalleFactura
        { provide: TblDetalleFacturaGateway, useClass: TblDetalleFacturaApiService },
        // TblFactura
        { provide: TblFacturaGateway, useClass: TblFacturaApiService },
        ConfigService,
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
