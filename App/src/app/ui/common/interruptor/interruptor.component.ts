import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
  selector: 'app-interruptor',
  templateUrl: './interruptor.component.html',
  styleUrls: ['./interruptor.component.scss'],
})
export class InterruptorComponent implements OnInit {
  @Input() labelLeft: string = 'Off';
  @Input() labelRight: string = 'On';
  @Input() estadoInicial: boolean = false; // Valor por defecto es false
  @Output() estadoCambiado = new EventEmitter<boolean>();

  estadoInterruptor: boolean = false;

  ngOnInit() {
    // Inicializar el estado del interruptor con el valor de estadoInicial
    this.estadoInterruptor = this.estadoInicial;
  }

  toggleInterruptor() {
    this.estadoInterruptor = !this.estadoInterruptor;
    this.estadoCambiado.emit(this.estadoInterruptor);
  }
}
