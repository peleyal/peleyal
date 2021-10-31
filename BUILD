load("@rules_proto//proto:defs.bzl", "proto_library")
load("@com_github_grpc_grpc//bazel:python_rules.bzl", "py_proto_library")

package(
    default_visibility = ["//visibility:public"],
)

proto_library(
    name = "my_proto",
    srcs = ["my_proto.proto"],
    visibility = ["//visibility:public"],
    deps = [
    ],
)

py_proto_library(
    name = "my_proto_py_proto",
    deps = [
        ":my_proto",
    ],
)
