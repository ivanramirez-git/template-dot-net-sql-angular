import { TblCliente } from "../tbl-cliente/tbl-cliente";
import { TblDetalleFactura } from "../tbl-detalle-factura/tbl-detalle-factura";

export interface TblFactura {
  id: number;
  fechaEmisionFactura: Date;
  idCliente: number | null;
  numeroDeFactura: number | null;
  numeroDeProductos: number;
  subTotalFactura: number;
  totalImpuestos: number;
  totalFactura: number;
  createdAt: Date;
  updatedAt: Date;
  deletedAt: Date | null;
  cliente: TblCliente | null;
  detalleFactura: TblDetalleFactura[];
}
