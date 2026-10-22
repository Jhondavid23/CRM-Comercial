import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ClientDetailComponent } from '../../components/client/client-detail/client-detail.component';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-clients-page',
  templateUrl: './clients-page.component.html',
  styleUrl: './clients-page.component.css'
})
export class ClientsPageComponent {

  @Input()
  public termBusqueda:string = "";

  constructor(
    private clientService: ClienteService,
    private dialog: MatDialog
  ){}

  emitValue(value: string){
    this.termBusqueda = value;
  }

  openDetailClient(){
    this.clientService.openClienDetail();
  }


}
