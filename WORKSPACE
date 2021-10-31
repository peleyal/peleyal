load("@bazel_tools//tools/build_defs/repo:git.bzl", "git_repository")
load("@bazel_tools//tools/build_defs/repo:http.bzl", "http_archive")

# Add external workspace dependencies

http_archive(
    name = "rules_python",
    sha256 = "e46612e9bb0dae8745de6a0643be69e8665a03f63163ac6610c210e80d14c3e4",
    url = "https://github.com/bazelbuild/rules_python/releases/download/0.0.3/rules_python-0.0.3.tar.gz",
)

# This can't be a version which isn't supported by :rules_proto
git_repository(
    name = "com_google_protobuf",
    tag = "v3.19.1",
    #commit = "89b14b1d16eba4d44af43256fc45b24a6a348557",  # v3.18.0
    remote = "https://github.com/protocolbuffers/protobuf",
    #shallow_since = "1631638108 -0700",
)

git_repository(
    name = "com_google_googleapis",
    commit = "84357f423fee6a61bafbcf390d42d4fe6afd9cf8",  # HEAD on 05/24/2021
    remote = "https://github.com/googleapis/googleapis",
    shallow_since = "1621887385 +0000",
)

git_repository(
    name = "subpar",
    commit = "35bb9f0092f71ea56b742a520602da9b3638a24f",  # v2.0.0
    remote = "https://github.com/google/subpar",
    shallow_since = "1557863961 -0400",
)

# Uses proto version 3.13.0
http_archive(
    name = "rules_proto",
    sha256 = "9fc210a34f0f9e7cc31598d109b5d069ef44911a82f507d5a88716db171615a8",
    strip_prefix = "rules_proto-f7a30f6f80006b591fa7c437fe5a951eb10bcbcf",
    urls = [
        "https://mirror.bazel.build/github.com/bazelbuild/rules_proto/archive/97d8af4dc474595af3900dd85cb3a29ad28cc313.tar.gz",
        "https://github.com/bazelbuild/rules_proto/archive/f7a30f6f80006b591fa7c437fe5a951eb10bcbcf.tar.gz",
    ],
)

# NOTE: We use the unofficial py_grpc_library and py_proto_library rules from
# gRPC for building our protos. As of 05/24/2020, there is no official source
# of python proto rules for Bazel.
git_repository(
    name = "com_github_grpc_grpc",
    commit = "54dc182082db941aa67c7c3f93ad858c99a16d7d",  # v1.38.0
    remote = "https://github.com/grpc/grpc",
    shallow_since = "1621536188 -0400",
)

http_archive(
    name = "io_bazel_rules_docker",
    sha256 = "4521794f0fba2e20f3bf15846ab5e01d5332e587e9ce81629c7f96c793bb7036",
    strip_prefix = "rules_docker-0.14.4",
    urls = ["https://github.com/bazelbuild/rules_docker/releases/download/v0.14.4/rules_docker-v0.14.4.tar.gz"],
)

http_archive(
    name = "io_bazel_rules_go",
    sha256 = "69de5c704a05ff37862f7e0f5534d4f479418afc21806c887db544a316f3cb6b",
    urls = [
        "https://mirror.bazel.build/github.com/bazelbuild/rules_go/releases/download/v0.27.0/rules_go-v0.27.0.tar.gz",
        "https://github.com/bazelbuild/rules_go/releases/download/v0.27.0/rules_go-v0.27.0.tar.gz",
    ],
)

http_archive(
    name = "bazel_gazelle",
    sha256 = "62ca106be173579c0a167deb23358fdfe71ffa1e4cfdddf5582af26520f1c66f",
    urls = [
        "https://mirror.bazel.build/github.com/bazelbuild/bazel-gazelle/releases/download/v0.23.0/bazel-gazelle-v0.23.0.tar.gz",
        "https://github.com/bazelbuild/bazel-gazelle/releases/download/v0.23.0/bazel-gazelle-v0.23.0.tar.gz",
    ],
)

load("@io_bazel_rules_go//go:deps.bzl", "go_register_toolchains", "go_rules_dependencies")
load("@bazel_gazelle//:deps.bzl", "gazelle_dependencies", "go_repository")

go_rules_dependencies()

# As of 05/24/21 using 1.16 causes build failures.
go_register_toolchains(version = "1.15")

gazelle_dependencies()

# Setup all remote workspaces.

load("@rules_python//experimental/rules_python_external:repositories.bzl", "rules_python_external_dependencies")

rules_python_external_dependencies()

load("@rules_python//experimental/rules_python_external:defs.bzl", "pip_install")

# Load all python packages used in this workspace.
pip_install(
    name = "py_deps",
    requirements = "//:requirements.txt",
)

load("@com_google_protobuf//:protobuf_deps.bzl", "protobuf_deps")

# Add six before call to protobuf_deps() and grpc_deps() below, to avoid adding an older six in
# https://github.com/protocolbuffers/protobuf/blob/v3.12.2/protobuf_deps.bzl#L32 and/or
# https://github.com/grpc/grpc/blob/v1.29.1/bazel/grpc_python_deps.bzl#L15
# The version of six here should be kept in sync with the version in requirements.txt.
http_archive(
    name = "six",
    build_file = "six.BUILD",
    sha256 = "30639c035cdb23534cd4aa2dd52c3bf48f06e5f4a941509c8bafd8ce11080259",
    url = "https://files.pythonhosted.org/packages/6b/34/415834bfdafca3c5f451532e8a8d9ba89a21c9743a0c59fbd0205c7f9426/six-1.15.0.tar.gz",
)

protobuf_deps()

load(
    "@io_bazel_rules_docker//repositories:repositories.bzl",
    container_repositories = "repositories",
)

container_repositories()

load("@io_bazel_rules_docker//repositories:deps.bzl", container_deps = "deps")

container_deps()

load("@io_bazel_rules_docker//container:pull.bzl", "container_pull")

container_pull(
    name = "alpine_linux_amd64",
    digest = "sha256:954b378c375d852eb3c63ab88978f640b4348b01c1b3456a024a81536dafbbf4",
    registry = "index.docker.io",
    repository = "library/alpine",
    tag = "3.8",
)

load("@io_bazel_rules_docker//repositories:pip_repositories.bzl", "pip_deps")

pip_deps()

# This pulls from a specified digest, so our python bazel images can rely on
# their underlying python version not changing and breaking them.
container_pull(
    name = "python_buster_base",
    # Digest is python:3.9-slim-buster for amd64 as of 2021/07/16 as determined
    # by running `docker manifest inspect python:3.9-slim-buster`.
    digest = "sha256:ec7a755e6313da2f7db02d8e82f6b0813b176f06c5622174c8ab45feefc8096d",
    registry = "index.docker.io",
    repository = "library/python",
)

load(
    "@io_bazel_rules_docker//python3:image.bzl",
    _py_image_repos = "repositories",
)

_py_image_repos()

load(
    "@io_bazel_rules_docker//go:image.bzl",
    _go_image_repos = "repositories",
)

_go_image_repos()

load("@com_google_googleapis//:repository_rules.bzl", "switched_rules_by_language")

switched_rules_by_language(
    name = "com_google_googleapis_imports",
    go = True,
    python = True,
)

# Required for using Go proto and gRPC rules, as well as for
# go libraries that depend on them such as the Google API client
# libraries.
go_repository(
    name = "org_golang_google_grpc",
    build_file_proto_mode = "disable",
    importpath = "google.golang.org/grpc",
    sum = "h1:f+PlOh7QV4iIJkPrx5NQ7qaNGFQ3OTse67yaDHfju4E=",
    version = "v1.41.0",
)

# NOTE: grpc pulls in dependencies transitively that we directly depend on.
# To ensure we load the latest versions, we load grpc dependencies last.
load("@com_github_grpc_grpc//bazel:grpc_deps.bzl", "grpc_deps")

grpc_deps()

load("//:repositories.bzl", "go_repositories")

# gazelle:repository_macro repositories.bzl%go_repositories
go_repositories()
