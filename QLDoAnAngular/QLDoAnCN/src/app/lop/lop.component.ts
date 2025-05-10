import { Component, OnInit } from '@angular/core';
import { LopService } from '../services/lop.service';
import { Lop } from '../models/lop.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';
import { ChuyenNganhService } from '../services/chuyennganh.service';

@Component({
  selector: 'app-lop',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './lop.component.html',
  styleUrl: './lop.component.css'
})
export class LopComponent implements OnInit {
  danhSachLop: Lop[] = [];
  selectedLop: Lop | null = null;
  isAddingNew: boolean = false;
  newLop: Lop = { maLop: '', tenLop: '', khoaHoc: '', maChuyenNganh: '' };
  isEditing: boolean = false;
  editingLop: Lop | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  chuyenNganhs: { maChuyenNganh: string, tenChuyenNganh: string }[] = [];

  constructor(private lopService: LopService, private chuyenNganhService: ChuyenNganhService) { }

  ngOnInit(): void {
    this.loadDanhSachLop();
    this.loadDanhSachChuyenNganh();
  }

  loadDanhSachLop(): void {
    this.lopService.getAllLop().subscribe({
      next: (data) => {
        this.danhSachLop = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách lớp.';
        console.error('Lỗi tải danh sách lớp', error);
      }
    });
  }

  loadDanhSachChuyenNganh(): void {
    this.chuyenNganhService.getAllChuyenNganh().subscribe({
      next: (data) => {
        this.chuyenNganhs = data;
      },
      error: (error) => {
        console.error('Lỗi khi tải danh sách chuyên ngành', error);
        this.errorMessage = 'Lỗi khi tải danh sách chuyên ngành.';
      }
    });
  }

  hienThiChiTiet(lop: Lop): void {
    this.selectedLop = lop;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newLop = { maLop: '', tenLop: '', khoaHoc: '', maChuyenNganh: '' };
  }

  batDauChinhSua(lop: Lop): void {
    this.editingLop = { ...lop };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedLop = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingLop = null;
  }

  themLop(): void {
    this.lopService.addLop(this.newLop).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm lớp thành công.';
        this.loadDanhSachLop();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm lớp.';
        console.error('Lỗi thêm lớp', error);
      }
    });
  }

  capNhatLop(): void {
    if (this.editingLop) {
      this.lopService.updateLop(this.editingLop.maLop, this.editingLop).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật lớp thành công.';
          this.loadDanhSachLop();
          this.isEditing = false;
          this.editingLop = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật lớp.';
          console.error('Lỗi cập nhật lớp', error);
        }
      });
    }
  }

  huyThemMoi(): void {
    this.isAddingNew = false;
  }

  huyChinhSua(): void {
    this.isEditing = false;
    this.editingLop = null;
  }

  xoaLop(maLop: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa lớp này?')) {
      this.lopService.deleteLop(maLop).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa lớp thành công.';
          this.loadDanhSachLop();
          this.selectedLop = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi xóa lớp.';
          console.error('Lỗi xóa lớp', error);
        }
      });
    }
  }
}