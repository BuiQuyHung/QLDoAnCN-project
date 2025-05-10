import { Component, OnInit } from '@angular/core';
import { BoMonService } from '../services/bomon.service';
import { BoMon } from '../models/bomon.model';
import { KhoaService } from '../services/khoa.service';
import { Khoa } from '../models/khoa.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';

@Component({
  selector: 'app-bo-mon',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './bomon.component.html',
  styleUrl: './bomon.component.css'
})
export class BoMonComponent implements OnInit {
  danhSachBoMon: BoMon[] = [];
  selectedBoMon: BoMon | null = null;
  isAddingNewBoMon: boolean = false;
  newBoMon: BoMon = { maBoMon: '', tenBoMon: '', maKhoa: '' };
  isEditingBoMon: boolean = false;
  editingBoMon: BoMon | null = null;
  isViewingDetailsBoMon: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';
  danhSachKhoa: Khoa[] = [];

  constructor(private boMonService: BoMonService,
              private khoaService: KhoaService) { }

  ngOnInit(): void {
    this.loadDanhSachBoMon();
    this.loadDanhSachKhoa();
  }

  loadDanhSachBoMon(): void {
    this.boMonService.getAllBoMons().subscribe({
      next: (data) => {
        this.danhSachBoMon = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách bộ môn.';
        console.error('Lỗi tải danh sách bộ môn', error);
      }
    });
  }

  loadDanhSachKhoa(): void {
    this.khoaService.getAllKhoa().subscribe({
      next: (data) => {
        this.danhSachKhoa = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách khoa.';
        console.error('Lỗi tải danh sách khoa', error);
      }
    });
  }

  hienThiChiTietBoMon(boMon: BoMon): void {
    this.selectedBoMon = boMon;
    this.isViewingDetailsBoMon = true;
  }

  batDauThemMoiBoMon(): void {
    this.isAddingNewBoMon = true;
    this.newBoMon = { maBoMon: '', tenBoMon: '', maKhoa: '' };
  }

  dongPopupChiTietBoMon(): void {
    this.isViewingDetailsBoMon = false;
    this.selectedBoMon = null;
  }

  dongPopupThemMoiBoMon(): void {
    this.isAddingNewBoMon = false;
  }

  dongPopupChinhSuaBoMon(): void {
    this.isEditingBoMon = false;
    this.editingBoMon = null;
  }

  themBoMon(): void {
    this.boMonService.addBoMon(this.newBoMon).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm bộ môn thành công.';
        this.loadDanhSachBoMon();
        this.isAddingNewBoMon = false; // Đóng popup sau khi thêm
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm bộ môn.';
        console.error('Lỗi thêm bộ môn', error);
      }
    });
  }

  batDauChinhSuaBoMon(boMon: BoMon): void {
    this.isEditingBoMon = true;
    this.editingBoMon = { ...boMon };
  }

  capNhatBoMon(): void {
    if (this.editingBoMon) {
      this.boMonService.updateBoMon(this.editingBoMon.maBoMon, this.editingBoMon).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật bộ môn thành công.';
          this.loadDanhSachBoMon();
          this.isEditingBoMon = false; // Đóng popup sau khi cập nhật
          this.editingBoMon = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật bộ môn.';
          console.error('Lỗi cập nhật bộ môn', error);
        }
      });
    }
  }

  huyThemMoiBoMon(): void {
    this.isAddingNewBoMon = false; // Đóng popup khi hủy
  }

  huyChinhSuaBoMon(): void {
    this.isEditingBoMon = false; // Đóng popup khi hủy
    this.editingBoMon = null;
  }

  xoaBoMon(maBoMon: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa bộ môn này?')) {
      this.boMonService.deleteBoMon(maBoMon).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa bộ môn thành công.';
          this.loadDanhSachBoMon();
          this.selectedBoMon = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi xóa bộ môn.';
          console.error('Lỗi xóa bộ môn', error);
        }
      });
    }
  }
}