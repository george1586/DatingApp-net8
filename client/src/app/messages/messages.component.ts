import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent implements OnInit{
  accountService=inject(AccountService);
  public flag:any;
  ngOnInit(): void {
    this.accountService.GetSettings().subscribe(flag => { this.flag=flag;});
  }
}
