<template>
  <div>
    <div>
      <h1 class="display-4 text-center mt-5">Listado de tareas</h1>
    </div>
    <div class="row">
      <div class="col-lg-8 offset-lg-2">
        <div class="card mt-4">
          <div class="card-body">
            <div class="input-group">
              <input
                type="text"
                name=""
                id=""
                class="form-control form-control-lg"
                placeholder="Agregue una tarea"
                v-model="tarea"
              />
              <div class="input-group-append">
                <button
                  class="btn btn-success btn-lg"
                  v-on:click="agregarTarea()"
                >
                  Agregar
                </button>
              </div>
            </div>
            <br />
            <div class="text-center">
              <div
                v-if="loading"
                class="spinner-border text-success"
                role="status"
              >
                <span class="sr-only">Loading...</span>
              </div>
            </div>
            <h5 v-if="listTareas.length == 0">No hay tareas que realizar</h5>
            <ul v-if="!loading" class="list-group mt-2">
              <li
                v-for="(tarea, index) of listTareas"
                :key="index"
                class="list-group-item d-flex justify-content-between"
              >
                <span
                  class="cursor"
                  v-on:click="editarTarea(tarea, tarea.id)"
                  v-bind:class="{ 'text-success': tarea.estado }"
                >
                  <i
                    v-bind:class="[
                      tarea.estado
                        ? 'fas fa-check-circle'
                        : 'fas fa-circle-notch',
                    ]"
                  ></i>
                </span>
                {{ tarea.nombre }}
                <span
                  class="text-danger cursor"
                  v-on:click="eliminarTarea(tarea.id)"
                >
                  <i class="fas fa-trash-alt"></i>
                </span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "Tarea",
  data() {
    return {
      tarea: "",
      urlApi: "https://localhost:5001/api/Tarea",
      loading: false,
      listTareas: [],
    };
  },
  methods: {
    agregarTarea() {
      const tarea = {
        nombre: this.tarea,
        estado: false,
      };
      //this.listTareas.push(tarea);
      this.loading = true;
      axios
        .post(this.urlApi + "/", tarea)
        .then(() => {
          this.obtenerTareas();
          this.loading = false;
        })
        .catch((error) => {
          console.error(error);
          this.loading = false;
        });
      this.tarea = "";
    },
    eliminarTarea(id) {
      //this.listTareas.splice(index, 1);
      this.loading = true;
      axios
        .delete(this.urlApi + "/" + id)
        .then(() => {
          this.obtenerTareas();
          this.loading = false;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    editarTarea(tarea, id) {
      //this.listTareas[index].estado = !tarea.estado;
      this.loading = true;
      axios
        .put(this.urlApi + "/" + id, tarea)
        .then(() => {
          this.obtenerTareas();
          this.loading = false;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    obtenerTareas() {
      axios
        .get(this.urlApi)
        .then((response) => {
          this.listTareas = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },
  created: function () {
    this.obtenerTareas();
  },
};
</script>

<style>
.cursor {
  cursor: pointer;
}
</style>
